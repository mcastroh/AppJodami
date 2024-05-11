using Jodami.AppWeb.Models.Dto;
using Jodami.AppWeb.Models.ViewModels;
using Jodami.BLL.Interfaces;
using Jodami.DAL.DBContext;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace Jodami.AppWeb.Controllers
{
    public class FormaPagosController : Controller
    {
        private readonly DbJodamiContext _contexto;
        private readonly IGenericService<SocioFormaPago> _svrSocioFormaPago;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Usuario _sessionUsuario;

        #region Constructor 

        public FormaPagosController(
            DbJodamiContext contexto,
            IGenericService<SocioFormaPago> svrSocioFormaPago,
            IHttpContextAccessor httpContextAccessor)
        {
            _contexto = contexto;
            _svrSocioFormaPago = svrSocioFormaPago;
            _httpContextAccessor = httpContextAccessor;
            _sessionUsuario = System.Text.Json.JsonSerializer.Deserialize<Usuario>(_httpContextAccessor.HttpContext.Session.GetString("sessionUsuario"));
        }

        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index(int idSocio, string tipoSocioOrigen, string controladorOrigen, string accionOrigen)
        {
            var modelo = await ETL_FormaPagos(idSocio, tipoSocioOrigen, controladorOrigen, accionOrigen);
            return View(modelo);
        }

        #endregion

        #region Adicionar => HttpPost

        [HttpPost]
        public async Task<IActionResult> Guardar(DtoFormaPagos modelo)
        {
            var lstSocioFormaPago = await _contexto.SocioFormaPago.Where(x => x.IdSocio == modelo.IdSocio).AsNoTracking().ToListAsync();
            var datosSeleccionados = modelo.vmTipoFormaPago.Where(x => x.SEL).ToList();

            //
            // Existen datos asignados y se han desmarcado todos
            if (datosSeleccionados.Count == 0 && lstSocioFormaPago.Count != 0)
            {
                foreach (var item in lstSocioFormaPago)
                {
                    var obj = await _svrSocioFormaPago.Delete(x => x.IdFormaPago == item.IdFormaPago);
                }

                return RedirectToAction(modelo.AccionOrigen, modelo.ControladorOrigen, new { idSocio = modelo.IdSocio, tipoSocioOrigen = modelo.TipoSocio, controladorOrigen = modelo.ControladorOrigen, accionOrigen = modelo.AccionOrigen });
            }

            //
            // Eliminar los desmarcados
            var fpEliminar = modelo.vmTipoFormaPago.Where(x => x.SeleccionInicial && !x.SEL).ToList();

            foreach (var ite in fpEliminar.ToList())
            {
                var q = lstSocioFormaPago.FirstOrDefault(x => x.IdTipoFormaPago == ite.IdTipoFormaPago);
                var obj = await _svrSocioFormaPago.Delete(x => x.IdTipoFormaPago == q.IdTipoFormaPago);
            }

            //
            // Nuevos elementos
            var fpNuevos = modelo.vmTipoFormaPago.Where(x => !x.SeleccionInicial && x.SEL).ToList();

            foreach (var ite in fpNuevos.ToList())
            {
                var data = new SocioFormaPago()
                {
                    IdSocio = modelo.IdSocio,
                    IdTipoFormaPago = ite.IdTipoFormaPago,
                    EsActivo = true,
                    UsuarioName = _sessionUsuario.Nombre,
                    FechaRegistro = DateTime.Now
                };

                var obj = await _svrSocioFormaPago.Insert(data);
            }

            return RedirectToAction(modelo.AccionOrigen, modelo.ControladorOrigen, new { idSocio = modelo.IdSocio, tipoSocioOrigen = modelo.TipoSocio, controladorOrigen = modelo.ControladorOrigen, accionOrigen = modelo.AccionOrigen });
        }

        #endregion

        #region Método  => Listar PDF   

        public async Task<IActionResult> ListarPDF(int idSocio, string tipoSocioOrigen, string controladorOrigen, string accionOrigen)
        {
            var query = await ETL_FormaPagos(idSocio, tipoSocioOrigen, controladorOrigen, accionOrigen);
            string titulo = "Formas de Pago " + tipoSocioOrigen;
            string protocolo = Request.IsHttps ? "Https" : "Http";
            string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
            string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

            return new ViewAsPdf("ListarPDF", query)
            {
                FileName = $"Formas de Pago {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
                CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
            };
        }

        #endregion

        #region ETL =>  Formas de Pago

        public async Task<DtoFormaPagos> ETL_FormaPagos(int idSocio, string tipoSocioOrigen, string controladorOrigen, string accionOrigen)
        {
            var formaPagos = new List<VMTipoFormaPago>();
            var proveedor = await _contexto.Socio.AsNoTracking().Where(x => x.IdSocio == idSocio).Include(e => e.IdTipoDcmtoIdentidadNavigation).FirstOrDefaultAsync();
            var lstLstTipoFormaPago = await _contexto.TipoFormaPago.AsNoTracking().ToListAsync();
            var lstSocioFormaPago = await _contexto.SocioFormaPago
                                                .Where(x => x.IdSocio == idSocio)
                                                .AsNoTracking()
                                                .Include(e => e.IdTipoFormaPagoNavigation)
                                                .ToListAsync();

            foreach (var item in lstLstTipoFormaPago)
            {
                var huboSel = lstSocioFormaPago.FirstOrDefault(x => x.IdTipoFormaPago == item.IdTipoFormaPago);

                var obj = new VMTipoFormaPago()
                {
                    IdTipoFormaPago = item.IdTipoFormaPago,
                    Codigo = item.Codigo,
                    Descripcion = item.Descripcion,
                    DiasDePago = item.DiasDePago,
                    EsActivo = item.EsActivo,
                    SEL = huboSel == null ? false : true,
                    SeleccionInicial = huboSel == null ? false : true
                };

                formaPagos.Add(obj);
            }

            string tipoDcmto = tipoSocioOrigen + ": " + proveedor.IdTipoDcmtoIdentidadNavigation.Simbolo + " " + proveedor.NumeroDcmtoIdentidad;
            string nombres = tipoDcmto == "RUC" ? proveedor.RazonSocial : $"{proveedor.ApellidoPaterno} {proveedor.ApellidoMaterno} {proveedor.PrimerNombre} {proveedor.SegundoNombre}";
            string situacion = string.Empty;

            if (proveedor.EsActivo)
            {
                situacion = "Activo";
                if (proveedor.FechaInicioOperaciones.HasValue)
                    situacion = $"Activo desde el {proveedor.FechaInicioOperaciones.Value.ToString("dd/MM/yyyy")}";
            }
            else
            {
                situacion = "Inactivo";
                if (proveedor.FechaBaja.HasValue)
                    situacion = $"Inactivo desde el {proveedor.FechaBaja.Value.ToString("dd/MM/yyyy")}";
            }

            var dto = new DtoFormaPagos()
            {
                IdSocio = idSocio,
                TipoNroDcmto = tipoDcmto,
                Nombres = nombres,
                Situacion = situacion,
                TipoSocio = tipoSocioOrigen,
                ControladorOrigen = controladorOrigen,
                AccionOrigen = accionOrigen,
                vmTipoFormaPago = formaPagos
            };

            return dto;
        }

        #endregion

    }
}