using Jodami.AppWeb.Models.Dto;
using Jodami.BLL.Interfaces;
using Jodami.DAL.DBContext;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace Jodami.AppWeb.Controllers
{
    public class DireccionesController : Controller
    {
        private readonly DbJodamiContext _contexto;
        private readonly IGenericService<Direccion> _svrDireccion;
        private readonly IGenericService<SocioDireccion> _svrSocioDireccion;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Usuario _sessionUsuario;             

        #region Constructor 

        public DireccionesController(
            DbJodamiContext contexto, 
            IGenericService<Direccion> svrDireccion, 
            IGenericService<SocioDireccion> svrSocioDireccion, 
            IHttpContextAccessor httpContextAccessor)
        {           
            _contexto = contexto;
            _svrDireccion = svrDireccion;
            _svrSocioDireccion = svrSocioDireccion;
            _httpContextAccessor = httpContextAccessor;
            _sessionUsuario = System.Text.Json.JsonSerializer.Deserialize<Usuario>(_httpContextAccessor.HttpContext.Session.GetString("sessionUsuario"));
        }

        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index(int idSocio, string tipoSocioOrigen, string controladorOrigen, string accionOrigen)
        {
            var modelo = await ETL_Direcciones(idSocio, tipoSocioOrigen, controladorOrigen, accionOrigen);
            return View(modelo);
        }

        #endregion

        #region Adicionar => HttpPost

        [HttpPost]
        public async Task<IActionResult> Adicionar(DtoDirecciones modelo)
        {
            var tipoVia = modelo.NewDireccion.IdTipoVia.HasValue ? (await _contexto.TipoVia.AsNoTracking().FirstOrDefaultAsync(x=> x.IdTipoVia == modelo.NewDireccion.IdTipoVia.Value)).Descripcion : string.Empty;
            var tipoZona = modelo.NewDireccion.IdTipoZona != 0 ? (await _contexto.TipoZona.AsNoTracking().FirstOrDefaultAsync(x => x.IdTipoZona == modelo.NewDireccion.IdTipoZona)).Descripcion : string.Empty;
            var lstDepartamentos = (await _contexto.Departamento.AsNoTracking().FirstOrDefaultAsync(x => x.IdDepartamento == modelo.DepartamentoKey)).DepartamentoName;
            var lstProvincia = (await _contexto.Provincia.AsNoTracking().FirstOrDefaultAsync(x => x.IdProvincia == modelo.ProvinciaKey)).ProvinciaName;
            var lstDistrito = (await _contexto.Distrito.AsNoTracking().FirstOrDefaultAsync(x => x.IdDistrito == modelo.DistritoKey)).DistritoName;

            //
            // Add direccion
            var direccion = modelo.NewDireccion;
            direccion.IdDireccion = 0;
            direccion.IdDistrito = modelo.DistritoKey;
            direccion.NameDireccion = tipoVia;
            direccion.NameUbigeo = lstDistrito;
            direccion.EsActivo = true;
            direccion.UsuarioName = _sessionUsuario.Nombre;
            direccion.FechaRegistro = DateTime.Now;
            var objDireccion = await _svrDireccion.Insert(direccion);

            //
            // Add direccion del socio de negocio
            modelo.NewSocioDireccion.IdDireccion = objDireccion.IdDireccion;
            modelo.NewSocioDireccion.EsActivo = true;
            modelo.NewSocioDireccion.UsuarioName = _sessionUsuario.Nombre;
            modelo.NewSocioDireccion.FechaRegistro = DateTime.Now;

            var objSocioDireccion = await _svrSocioDireccion.Insert(modelo.NewSocioDireccion);
            return RedirectToAction("Index", new { idSocio = modelo.IdSocio, tipoSocioOrigen = modelo.TipoSocio, controladorOrigen = modelo.ControladorOrigen, accionOrigen = modelo.AccionOrigen });
        }

        #endregion

        #region Edit => HttpPost 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(DtoDirecciones modelo)
        {
            //modelo.NewContacto.UsuarioName = _sessionUsuario.Nombre;
            //modelo.NewContacto.FechaRegistro = DateTime.Now;
            //bool flgRetorno = await _svrSocioContacto.Update(modelo.NewContacto);
            return RedirectToAction("Index", new { idSocio = modelo.IdSocio, tipoSocioOrigen = modelo.TipoSocio, controladorOrigen = modelo.ControladorOrigen, accionOrigen = modelo.AccionOrigen });
        }

        #endregion

        #region Eliminar => HttpPost

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(DtoDirecciones modelo)
        {
            bool flgRetorno = await _svrSocioDireccion.Delete(x => x.IdSocio == modelo.IdSocio && x.IdDireccion == modelo.NewDireccion.IdDireccion);
            flgRetorno = await _svrDireccion.Delete(x => x.IdDireccion == modelo.NewDireccion.IdDireccion);
            return RedirectToAction("Index", new { idSocio = modelo.IdSocio, tipoSocioOrigen = modelo.TipoSocio, controladorOrigen = modelo.ControladorOrigen, accionOrigen = modelo.AccionOrigen });
        }

        #endregion

        #region Método  => Listar PDF   

        public async Task<IActionResult> ListarPDF(int idSocio, string tipoSocioOrigen, string controladorOrigen, string accionOrigen)
        {
            var query = await ETL_Direcciones(idSocio, tipoSocioOrigen, controladorOrigen, accionOrigen);

            string titulo = "Direcciones del " + tipoSocioOrigen;
            string protocolo = Request.IsHttps ? "Https" : "Http";
            string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
            string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

            return new ViewAsPdf("ListarPDF", query)
            {
                FileName = $"Direcciones {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
                CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
            };
        }

        #endregion

        #region ETL =>  Direcciones del ????

        public async Task<DtoDirecciones> ETL_Direcciones(int idSocio, string tipoSocioOrigen, string controladorOrigen, string accionOrigen)
        {
            var proveedor = await _contexto.Socio.AsNoTracking().Where(x => x.IdSocio == idSocio).Include(e => e.IdTipoDcmtoIdentidadNavigation).FirstOrDefaultAsync();
            var lstDirecciones = await _contexto.SocioDireccion
                                                .Where(x => x.IdSocio == idSocio)
                                                .AsNoTracking()
                                                .Include(e => e.IdDireccionNavigation)
                                                .Include(x => x.IdDireccionNavigation.IdTipoDireccionNavigation)
                                                .Include(x => x.IdDireccionNavigation.IdTipoViaNavigation)
                                                .Include(x => x.IdDireccionNavigation.IdTipoZonaNavigation)
                                                .Include(x => x.IdDireccionNavigation.IdDistritoNavigation)
                                                .Include(x => x.IdDireccionNavigation.IdDistritoNavigation.IdProvinciaNavigation)
                                                .Include(x => x.IdDireccionNavigation.IdDistritoNavigation.IdProvinciaNavigation.IdDepartamentoNavigation)
                                                .ToListAsync();            
            
            var lstTipoDirecciones = await _contexto.TipoDireccion.AsNoTracking().ToListAsync();
            var lstTipoVias = await _contexto.TipoVia.AsNoTracking().ToListAsync();
            var lstTipoZonas = await _contexto.TipoZona.AsNoTracking().ToListAsync();
            var lstDepartamentos = await _contexto.Departamento.AsNoTracking().ToListAsync();

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

            var dto = new DtoDirecciones()
            {
                IdSocio = idSocio,
                TipoNroDcmto = tipoDcmto,
                Nombres = nombres,
                Situacion = situacion,
                TipoSocio = tipoSocioOrigen,
                ControladorOrigen = controladorOrigen,
                AccionOrigen = accionOrigen,
                NewSocioDireccion = new SocioDireccion() { IdSocio = idSocio },
                NewDireccion = new Direccion(),
                LstSocioDirecciones = lstDirecciones,
                LstTipoDirecciones = lstTipoDirecciones,
                LstTipoVias = lstTipoVias,
                LstTipoZonas = lstTipoZonas,
                LstDepartamentos = lstDepartamentos

            };

            return dto;
        }

        #endregion

        #region GET => Departamentos

        public JsonResult getUbigeoDepartamento()
        {             
            var dataDepartamentos = _contexto.Departamento
                .AsNoTracking()
                .Where(x=> x.Provincia.Count != 0)
                .OrderBy(x => x.DepartamentoName).ToList(); 
            return Json(dataDepartamentos);
        }

        #endregion

        #region GET => Provincias por Departamento 

        public JsonResult getUbigeoProvinciasByDepartamento(int b_departamento)
        {
            var dataProvincias = _contexto.Provincia.Where(x=> x.IdDepartamento == b_departamento).AsNoTracking().OrderBy(x => x.ProvinciaName).ToList();
            return Json(dataProvincias);
        }

        #endregion

        #region GET => Distritos por Departamento y Provincia

        public JsonResult getUbigeoDistritosByDepartamentoAndProvincia(int b_provincia)
        {           
            var dataDistritos = _contexto.Distrito.Where(x => x.IdProvincia == b_provincia).AsNoTracking().OrderBy(x => x.DistritoName).ToList();
            return Json(dataDistritos);
        }

        #endregion 

    }
}