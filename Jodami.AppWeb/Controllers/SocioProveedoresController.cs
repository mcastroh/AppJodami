using AutoMapper;
using Jodami.AppWeb.Models.ViewModels;
using Jodami.AppWeb.Utilidades.Infraestructura;
using Jodami.AppWeb.Utilidades.Servicios;
using Jodami.BLL.Interfaces;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace Jodami.AppWeb.Controllers
{
    public class SocioProveedoresController : Controller
    {
        private readonly IMapper _mapper;

        private readonly IGenericService<Socio> _srvSocio;
        private readonly IGenericService<TipoSocio> _srvTipoSocio;
        private readonly IGenericService<TipoDocumentoIdentidad> _srvTipoDcmtoIdentidad;
        private readonly IGenericService<TipoCalificacion> _srvTipoCalificacion;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Usuario _sessionUsuario;

        #region Constructor 

        public SocioProveedoresController(
            IMapper mapper, 
            IGenericService<Socio> srvSocio, 
            IGenericService<TipoSocio> srvTipoSocio, 
            IGenericService<TipoDocumentoIdentidad> srvTipoDcmtoIdentidad,
            IGenericService<TipoCalificacion> srvTipoCalificacion,
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _srvSocio = srvSocio;
            _srvTipoSocio = srvTipoSocio;
            _srvTipoDcmtoIdentidad = srvTipoDcmtoIdentidad;
            _srvTipoCalificacion = srvTipoCalificacion;
            _httpContextAccessor = httpContextAccessor;
            _sessionUsuario = System.Text.Json.JsonSerializer.Deserialize<Usuario>(_httpContextAccessor.HttpContext.Session.GetString("sessionUsuario"));
        }

        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var query = await ETL_SociosComerciales();
            return View(query);
        }

        #endregion

        #region Adicionar => HttpPost

        [HttpPost]
        public async Task<IActionResult> Adicionar(VMSociosProveedores modelo)
        {
            var keyRUC = (await _srvTipoDcmtoIdentidad.GetById(x => x.Simbolo == KeysNames.TIPO_DCMTO_IDENTIDAD_RUC)).IdTipoDcmtoIdentidad;

            var socio = new Socio()
            {
                IdTipoSocio = modelo.IdTipoSocio,
                IdTipoDcmtoIdentidad = modelo.IdTipoDcmtoIdentidadAsignado,
                NumeroDcmtoIdentidad = modelo.NumeroDcmtoIdentidad,
                RazonSocial = keyRUC == modelo.IdTipoDcmtoIdentidadAsignado ? modelo.RazonSocial : string.Empty,
                ApellidoPaterno = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.ApellidoPaterno : string.Empty,
                ApellidoMaterno = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.ApellidoMaterno : string.Empty,
                PrimerNombre = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.PrimerNombre : string.Empty,
                SegundoNombre = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.SegundoNombre : string.Empty,
                Telefono = modelo.Telefono,
                Celular = modelo.Celular,
                PaginaWeb = modelo.PaginaWeb,
                Email = modelo.Email,
                IsAfectoRetencion = modelo.IsAfectoRetencion,
                IsAfectoPercepcion = modelo.IsAfectoPercepcion,
                IsBuenContribuyente = modelo.IsBuenContribuyente,
                IdTipoCalificacion = modelo.IdTipoCalificacion,
                ZonaPostal = modelo.ZonaPostal,
                FechaInicioOperaciones = modelo.FechaInicioOperaciones.HasValue ? modelo.FechaInicioOperaciones.Value : null,
                IdGrupoSocioNegocio = modelo.IdGrupoSocioNegocio.HasValue ? modelo.IdGrupoSocioNegocio : null,
                IdColaboradorAsignado = modelo.IdColaboradorAsignado.HasValue ? modelo.IdColaboradorAsignado : null,
                EsActivo = true,
                UsuarioName = _sessionUsuario.Nombre,
                FechaRegistro = DateTime.Now
            };

            var entity = await _srvSocio.Insert(socio);
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit => HttpPost 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(VMSociosProveedores modelo)
        {
            var keyRUC = (await _srvTipoDcmtoIdentidad.GetById(x => x.Simbolo == KeysNames.TIPO_DCMTO_IDENTIDAD_RUC)).IdTipoDcmtoIdentidad;
            var socio = await _srvSocio.GetById(x => x.IdSocio == modelo.IdSocio);
          
            socio.IdTipoSocio = modelo.IdTipoSocio;
            socio.IdTipoDcmtoIdentidad = modelo.IdTipoDcmtoIdentidadAsignado;
            socio.NumeroDcmtoIdentidad = modelo.NumeroDcmtoIdentidad;

            socio.RazonSocial = keyRUC == modelo.IdTipoDcmtoIdentidadAsignado ? modelo.RazonSocial : string.Empty;
            socio.ApellidoPaterno = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.ApellidoPaterno : string.Empty;
            socio.ApellidoMaterno = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.ApellidoMaterno : string.Empty;
            socio.PrimerNombre = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.PrimerNombre : string.Empty;
            socio.SegundoNombre = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.SegundoNombre : string.Empty;

            socio.Telefono = modelo.Telefono;
            socio.Celular = modelo.Celular;
            socio.Email = modelo.Email;
            socio.PaginaWeb = modelo.PaginaWeb;

            socio.IsAfectoRetencion = modelo.IsAfectoRetencion;
            socio.IsAfectoPercepcion = modelo.IsAfectoPercepcion;
            socio.IsBuenContribuyente = modelo.IsBuenContribuyente;
            socio.IdTipoCalificacion = modelo.IdTipoCalificacion;
            socio.ZonaPostal = modelo.ZonaPostal;
            socio.FechaInicioOperaciones = modelo.FechaInicioOperaciones.HasValue ? modelo.FechaInicioOperaciones.Value : null;

            socio.IdGrupoSocioNegocio = modelo.IdGrupoSocioNegocio.HasValue ? modelo.IdGrupoSocioNegocio : null;
            socio.IdColaboradorAsignado = modelo.IdColaboradorAsignado.HasValue ? modelo.IdColaboradorAsignado : null;

            socio.EsActivo = modelo.EsActivo;
            socio.UsuarioName = _sessionUsuario.Nombre;
            socio.FechaRegistro = DateTime.Now;

            bool flgRetorno = await _srvSocio.Update(socio);
            return RedirectToAction("Index");
        }

        #endregion

        #region Eliminar => HttpPost

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(VMSociosProveedores modelo)
        {
            bool flgRetorno = await _srvSocio.Delete(x => x.IdSocio == modelo.IdSocio);
            return RedirectToAction("Index");
        }

        #endregion

        #region Método  => Listar PDF   

        public async Task<IActionResult> ListarPDF()
        {
            var query = await ETL_SociosComerciales();

            string titulo = "Proveedores";
            string protocolo = Request.IsHttps ? "Https" : "Http";
            string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
            string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

            return new ViewAsPdf("ListarPDF", query)
            {
                FileName = $"Proveedores {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
                CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
            };
        }

        #endregion

        #region ETL =>  Proveedores

        public async Task<List<VMSociosProveedores>> ETL_SociosComerciales()
        { 
            ServicioTiposDeDocumentos svtTD = new ServicioTiposDeDocumentos(_srvTipoDcmtoIdentidad);
            var tipoDocumentoIdentidad = await svtTD.SrvTiposDeDocumentos();            
                             
            var tipoSocioGruposEconomicos = await _srvTipoSocio.GetById(x => x.Codigo == KeysNames.GRUPOS_ECONOMICOS);
            var tipoSocioColaboradores = await _srvTipoSocio.GetById(x => x.Codigo == KeysNames.COLABORADORES);
            var tipoSocioProveedores = await _srvTipoSocio.GetById(x => x.Codigo == KeysNames.PROVEEDORES);

            var entityGruposEconomicos = _mapper.Map<List<VMSociosGrupos>>(await _srvSocio.GetByFilter(x => x.IdTipoSocio == tipoSocioGruposEconomicos.IdTipoSocio));
            var entityColaboradores = _mapper.Map<List<VMSociosColaboradores>>(await _srvSocio.GetByFilter(x => x.IdTipoSocio == tipoSocioColaboradores.IdTipoSocio));
            var entityProveedores = _mapper.Map<List<VMSociosProveedores>>(await _srvSocio.GetByFilter(x => x.IdTipoSocio == tipoSocioProveedores.IdTipoSocio));
            
            var entityTipoCalificacion = _mapper.Map<List<VMTipoCalificacion>>(await _srvTipoCalificacion.GetAll());

            foreach (var item in entityTipoCalificacion)
            {
                item.CodigoAndDescripcion = item.Codigo + " - " + item.Descripcion;
            }

            foreach (var item in entityColaboradores)
            {
                item.ApellidosAndNombres = item.ApellidoPaterno + " " + item.ApellidoMaterno + " " + item.PrimerNombre + " " + item.SegundoNombre;
            }

            foreach ( var item in entityProveedores) 
            {
                item.IdTipoDcmtoIdentidadAsignado = item.IdTipoDcmtoIdentidad;
                item.CodigoTipoDcmto = tipoDocumentoIdentidad.FirstOrDefault(x=> x.IdTipoDcmtoIdentidad == item.IdTipoDcmtoIdentidad).Simbolo;
                item.NameTipoDcmto = tipoDocumentoIdentidad.FirstOrDefault(x => x.IdTipoDcmtoIdentidad == item.IdTipoDcmtoIdentidad).Descripcion;
                item.TiposDcmtoIdentidad = tipoDocumentoIdentidad;
                item.NombreRazonSocial = item.CodigoTipoDcmto == KeysNames.TIPO_DCMTO_IDENTIDAD_RUC ? item.RazonSocial : item.ApellidoPaterno + " " + item.ApellidoMaterno + "" + item.PrimerNombre + "" + item.SegundoNombre;
                item.nav_GrupoEconomico = entityGruposEconomicos;
                item.nav_Colaboradores = entityColaboradores;
                item.nav_Calificacion = entityTipoCalificacion;

                //item.ApellidoPaterno = item.ApellidoPaterno;
                //item.ApellidoMaterno = item.ApellidoMaterno;
                //item.PrimerNombre = item.PrimerNombre;
                //item.SegundoNombre = item.SegundoNombre;
                //item.IdSocio = item.IdSocio;

            }             

            ViewBag.TipoDocumentoIdentidad = tipoDocumentoIdentidad.ToList();
            ViewBag.TipoSocio = tipoSocioProveedores;
            ViewBag.DcmtoDefault = tipoDocumentoIdentidad.FirstOrDefault(x => x.Simbolo == KeysNames.TIPO_DCMTO_IDENTIDAD_RUC);

            ViewBag.GrupoEconomico = entityGruposEconomicos.ToList();
            ViewBag.Colaboradores = entityColaboradores.ToList();
            ViewBag.Calificacion = entityTipoCalificacion.ToList(); 

            return entityProveedores;
        }

        #endregion


        #region Eliminar => HttpPost   
        //<a class="btn btn-secondary btn-sm" href="@Url.Action("Eliminar", "Contactos", new { idSocio = item.IdSocio, tipoSocioOrigen=@tipoSocioOrigen, controladorOrigen=@controladorOrigen, accionOrigen=@accionOrigen})"><i class="bi bi-justify"></i>Contactos</a>


         
        public async Task<IActionResult> EliminarProveedor(VMSociosProveedores modelo)
        {
            var keyRUC = (await _srvTipoDcmtoIdentidad.GetById(x => x.Simbolo == KeysNames.TIPO_DCMTO_IDENTIDAD_RUC)).IdTipoDcmtoIdentidad;
            var socio = await _srvSocio.GetById(x => x.IdSocio == modelo.IdSocio);

            socio.IdTipoSocio = modelo.IdTipoSocio;
            socio.IdTipoDcmtoIdentidad = modelo.IdTipoDcmtoIdentidadAsignado;
            socio.NumeroDcmtoIdentidad = modelo.NumeroDcmtoIdentidad;

            socio.RazonSocial = keyRUC == modelo.IdTipoDcmtoIdentidadAsignado ? modelo.RazonSocial : string.Empty;
            socio.ApellidoPaterno = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.ApellidoPaterno : string.Empty;
            socio.ApellidoMaterno = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.ApellidoMaterno : string.Empty;
            socio.PrimerNombre = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.PrimerNombre : string.Empty;
            socio.SegundoNombre = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.SegundoNombre : string.Empty;

            socio.Telefono = modelo.Telefono;
            socio.Celular = modelo.Celular;
            socio.Email = modelo.Email;
            socio.PaginaWeb = modelo.PaginaWeb;

            socio.IsAfectoRetencion = modelo.IsAfectoRetencion;
            socio.IsAfectoPercepcion = modelo.IsAfectoPercepcion;
            socio.IsBuenContribuyente = modelo.IsBuenContribuyente;
            socio.IdTipoCalificacion = modelo.IdTipoCalificacion;
            socio.ZonaPostal = modelo.ZonaPostal;
            socio.FechaInicioOperaciones = modelo.FechaInicioOperaciones.HasValue ? modelo.FechaInicioOperaciones.Value : null;

            socio.IdGrupoSocioNegocio = modelo.IdGrupoSocioNegocio.HasValue ? modelo.IdGrupoSocioNegocio : null;
            socio.IdColaboradorAsignado = modelo.IdColaboradorAsignado.HasValue ? modelo.IdColaboradorAsignado : null;

            socio.EsActivo = modelo.EsActivo;
            socio.UsuarioName = _sessionUsuario.Nombre;
            socio.FechaRegistro = DateTime.Now;

            bool flgRetorno = await _srvSocio.Update(socio);
            return View(socio);
        }

            
        //[HttpPost]      
        //public async Task<IActionResult> xEliminarProveedor(VMSociosProveedores modelo)
        //{ 
        //    return RedirectToAction("EliminarProveedor", "SocioProveedores", modelo);
        //}

        #endregion


    }
}