using Jodami.AppWeb.Models.Dto;
using Jodami.BLL.Interfaces;
using Jodami.DAL.DBContext;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace Jodami.AppWeb.Controllers
{
    public class ContactosController : Controller
    {
        private readonly DbJodamiContext _contexto;             
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Usuario _sessionUsuario;       
        private readonly IGenericService<SocioContacto> _svrSocioContacto;

        #region Constructor 

        public ContactosController(DbJodamiContext contexto, IGenericService<SocioContacto> svrSocioContacto, IHttpContextAccessor httpContextAccessor)
        {           
            _contexto = contexto;
            _svrSocioContacto = svrSocioContacto;
            _httpContextAccessor = httpContextAccessor;
            _sessionUsuario = System.Text.Json.JsonSerializer.Deserialize<Usuario>(_httpContextAccessor.HttpContext.Session.GetString("sessionUsuario"));
        }

        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index(int idSocio, string tipoSocioOrigen, string controladorOrigen, string accionOrigen)
        {
            var modelo = await ETL_ContactosDelProveedor(idSocio, tipoSocioOrigen, controladorOrigen, accionOrigen);
            return View(modelo);
        }

        #endregion

        #region Adicionar => HttpPost

        [HttpPost]
        public async Task<IActionResult> Adicionar(DtoContactos modelo)
        {
            modelo.NewContacto.IdContacto = 0;
            modelo.NewContacto.EsActivo = true;
            modelo.NewContacto.UsuarioName = _sessionUsuario.Nombre;
            modelo.NewContacto.FechaRegistro = DateTime.Now;
            var entity = await _svrSocioContacto.Insert(modelo.NewContacto);
            return RedirectToAction("Index", new { idSocio = modelo.IdSocio, tipoSocioOrigen = modelo.TipoSocio, controladorOrigen = modelo.ControladorOrigen, accionOrigen = modelo.AccionOrigen });
        }

        #endregion

        #region Edit => HttpPost 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(DtoContactos modelo)
        {            
            modelo.NewContacto.UsuarioName = _sessionUsuario.Nombre;
            modelo.NewContacto.FechaRegistro = DateTime.Now;
            bool flgRetorno = await _svrSocioContacto.Update(modelo.NewContacto);
            return RedirectToAction("Index", new { idSocio = modelo.IdSocio, tipoSocioOrigen = modelo.TipoSocio, controladorOrigen = modelo.ControladorOrigen, accionOrigen = modelo.AccionOrigen });
        }

        #endregion

        #region Eliminar => HttpPost

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(DtoContactos modelo)
        {
            bool flgRetorno = await _svrSocioContacto.Delete(x => x.IdContacto == modelo.NewContacto.IdContacto);
            return RedirectToAction("Index", new { idSocio = modelo.IdSocio, tipoSocioOrigen = modelo.TipoSocio, controladorOrigen = modelo.ControladorOrigen, accionOrigen = modelo.AccionOrigen });
        }

        #endregion

        #region Método  => Listar PDF   

        public async Task<IActionResult> ListarPDF(int idSocio, string tipoSocioOrigen, string controladorOrigen, string accionOrigen)
        {
            var query =  await ETL_ContactosDelProveedor(idSocio, tipoSocioOrigen, controladorOrigen, accionOrigen);

            string titulo = "Contactos del " + tipoSocioOrigen;
            string protocolo = Request.IsHttps ? "Https" : "Http";
            string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
            string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

            return new ViewAsPdf("ListarPDF", query)
            {
                FileName = $"Contactos {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
                CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
            };
        }

        #endregion

        #region ETL =>  Contactos del Proveedor

        public async Task<DtoContactos> ETL_ContactosDelProveedor(int idSocio, string tipoSocioOrigen, string controladorOrigen, string accionOrigen)
        {
            var proveedor = await _contexto.Socio.AsNoTracking().Where(x => x.IdSocio == idSocio).Include(e => e.IdTipoDcmtoIdentidadNavigation).FirstOrDefaultAsync();
            var contactos = await _contexto.SocioContacto.Where(x => x.IdSocio == idSocio).AsNoTracking().Include(e => e.IdCargoNavigation).Include(e => e.IdSocioNavigation).ToListAsync();
            var cargos = await _contexto.Cargo.AsNoTracking().ToListAsync();
            var tiposDcmtoIdentidad = await _contexto.TipoDocumentoIdentidad.AsNoTracking().ToListAsync();

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

            var dto = new DtoContactos()
            {
                IdSocio = idSocio,
                TipoNroDcmto = tipoDcmto,
                Nombres = nombres,
                Situacion = situacion,
                TipoSocio = tipoSocioOrigen,
                ControladorOrigen = controladorOrigen,
                AccionOrigen = accionOrigen,
                LstContactos = contactos,
                LstCargos = cargos,
                NewContacto = new SocioContacto() { IdSocio = idSocio }
            };

            return dto;
        }

        #endregion 


    }
}