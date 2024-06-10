using AutoMapper;
using Jodami.AppWeb.Models.ViewModels;
using Jodami.AppWeb.Utilidades.Infraestructura;
using Jodami.AppWeb.Utilidades.Servicios;
using Jodami.BLL.Interfaces;
using Jodami.DAL.DBContext;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jodami.AppWeb.Controllers
{
    public class SocioProveedoresController : Controller
    {
        #region Variables 

        private readonly DbJodamiContext _contexto;
        private readonly IGenericService<Socio> _srvSocio;
        private readonly Usuario _sessionUsuario;
        private readonly IGenericService<TipoDocumentoIdentidad> _srvTipoDcmtoIdentidad;       
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor 

        public SocioProveedoresController(
            DbJodamiContext contexto,
            IGenericService<Socio> srvSocio,
            IGenericService<TipoDocumentoIdentidad> srvTipoDcmtoIdentidad,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper           
           )
        {
            _contexto = contexto;
            _srvSocio = srvSocio;
            _srvTipoDcmtoIdentidad = srvTipoDcmtoIdentidad;
            _mapper = mapper; 
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

        [HttpGet]
        public async Task<IActionResult> Adicionar()
        {
            var svtTD = new ServicioTiposDeDocumentos(_srvTipoDcmtoIdentidad);
            var tipoDocumentoIdentidad = await svtTD.SrvTiposDeDocumentos();

            var tipoSocioGruposEconomicos = await _contexto.TipoSocio.AsNoTracking().FirstOrDefaultAsync(x => x.Codigo == KeysNames.GRUPOS_ECONOMICOS);
            var tipoSocioColaboradores = await _contexto.TipoSocio.AsNoTracking().FirstOrDefaultAsync(x => x.Codigo == KeysNames.COLABORADORES);
            var tipoSocioProveedores = await _contexto.TipoSocio.AsNoTracking().FirstOrDefaultAsync(x => x.Codigo == KeysNames.PROVEEDORES);

            var entityGruposEconomicos = _mapper.Map<List<VMSociosGrupos>>(await _contexto.Socio.AsNoTracking().Where(x => x.IdTipoSocio == tipoSocioGruposEconomicos.IdTipoSocio).ToListAsync());
            var entityColaboradores = _mapper.Map<List<VMSociosColaboradores>>(await _contexto.Socio.AsNoTracking().Where(x => x.IdTipoSocio == tipoSocioColaboradores.IdTipoSocio).ToListAsync());
            var entityProveedores = _mapper.Map<List<VMSociosProveedores>>(await _contexto.Socio.AsNoTracking().Where(x => x.IdTipoSocio == tipoSocioProveedores.IdTipoSocio).ToListAsync());

            var entityTipoCalificacion = _mapper.Map<List<VMTipoCalificacion>>(await _contexto.TipoCalificacion.ToListAsync());

            //foreach (var item in entityTipoCalificacion)
            //{
            //    item.CodigoAndDescripcion = item.Codigo + " - " + item.Descripcion;
            //}

            //foreach (var item in entityColaboradores)
            //{
            //    item.ApellidosAndNombres = item.ApellidoPaterno + " " + item.ApellidoMaterno + " " + item.PrimerNombre + " " + item.SegundoNombre;
            //}

            //foreach (var item in entityProveedores)
            //{
            //    item.IdTipoDcmtoIdentidadAsignado = item.IdTipoDcmtoIdentidad;
            //    item.CodigoTipoDcmto = tipoDocumentoIdentidad.FirstOrDefault(x => x.IdTipoDcmtoIdentidad == item.IdTipoDcmtoIdentidad).Simbolo;
            //    item.NameTipoDcmto = tipoDocumentoIdentidad.FirstOrDefault(x => x.IdTipoDcmtoIdentidad == item.IdTipoDcmtoIdentidad).Descripcion;
            //    item.TiposDcmtoIdentidad = tipoDocumentoIdentidad;
            //    item.NombreRazonSocial = item.CodigoTipoDcmto == KeysNames.TIPO_DCMTO_IDENTIDAD_RUC ? item.RazonSocial : item.ApellidoPaterno + " " + item.ApellidoMaterno + "" + item.PrimerNombre + "" + item.SegundoNombre;
            //    item.nav_GrupoEconomico = entityGruposEconomicos;
            //    item.nav_Colaboradores = entityColaboradores;
            //    item.nav_Calificacion = entityTipoCalificacion;
            //}

            ViewBag.TipoDocumentoIdentidad = tipoDocumentoIdentidad.ToList();
            ViewBag.TipoSocio = tipoSocioProveedores;
            ViewBag.DcmtoDefault = tipoDocumentoIdentidad.FirstOrDefault(x => x.Simbolo == KeysNames.TIPO_DCMTO_IDENTIDAD_RUC);

            ViewBag.GrupoEconomico = entityGruposEconomicos.ToList();
            ViewBag.Colaboradores = entityColaboradores.ToList();
            ViewBag.Calificacion = entityTipoCalificacion.ToList();

             

            var tipoSocio = (TipoSocio)ViewBag.TipoSocio;
            var tiposDcmtosIdentidad = (List<VMTipoDocumentoIdentidad>)ViewBag.TipoDocumentoIdentidad;
            var dcmtoDefault = (VMTipoDocumentoIdentidad)ViewBag.DcmtoDefault;

            var grupoEconomico = (List<VMSociosGrupos>)ViewBag.GrupoEconomico;
            var colaboradores = (List<VMSociosColaboradores>)ViewBag.Colaboradores;
            var calificacion = (List<VMTipoCalificacion>)ViewBag.Calificacion;

            var entity = new VMSociosProveedores()
            {
                IdSocio = 0,
                IdTipoSocio = tipoSocio.IdTipoSocio,
                IdTipoDcmtoIdentidad = dcmtoDefault.IdTipoDcmtoIdentidad,
                IdTipoDcmtoIdentidadAsignado = dcmtoDefault.IdTipoDcmtoIdentidad,
                NumeroDcmtoIdentidad = string.Empty,
                CodigoTipoDcmto = dcmtoDefault.Simbolo,
                NameTipoDcmto = dcmtoDefault.Descripcion,
                TiposDcmtoIdentidad = tiposDcmtosIdentidad,
                RazonSocial = string.Empty,
                ApellidoPaterno = string.Empty,
                ApellidoMaterno = string.Empty,
                PrimerNombre = string.Empty,
                SegundoNombre = string.Empty,
                Telefono = string.Empty,
                Celular = string.Empty,
                PaginaWeb = string.Empty,
                Email = string.Empty,
                KeyIdTipoDcmtoIdentidadRUC = dcmtoDefault.IdTipoDcmtoIdentidad,
                nav_GrupoEconomico = grupoEconomico,
                nav_Colaboradores = colaboradores,
                nav_Calificacion = calificacion,
                OperacionCRUD = "INSERT"
            };


            return View(entity);
        }

        #region Adicionar => HttpPost

        // 
        // Curso de Javascript - 4.08. Validación avanzada de formularios con HTML5 y Javascript
        // https://www.youtube.com/watch?v=a5QH6PSm-ew
        //


        [HttpPost]
        public async Task<IActionResult> Adicionar(VMSociosProveedores modelo)
        {
            if (ModelState.IsValid)
            {
                //var user = _userServies.getUserByEmailandPass(login.Email, login.Password);
                //if (user == null)
                if (modelo.RazonSocial is null)
                {
                    ModelState.AddModelError("RazonSocial", "email or password is wrong");
                }
            }
            else
                ModelState.AddModelError("RazonSocial", "email or password invalid");

            return PartialView("Add", modelo);


            var svtTD = new ServicioTiposDeDocumentos(_srvTipoDcmtoIdentidad);
            var tipoDcmtoIdent = (await svtTD.SrvTiposDeDocumentos()).FirstOrDefault(x => x.Simbolo == KeysNames.TIPO_DCMTO_IDENTIDAD_RUC);

            if (modelo.IdTipoDcmtoIdentidadAsignado == tipoDcmtoIdent.IdTipoDcmtoIdentidad)
            {
                if (modelo.RazonSocial is null)
                {
                    ViewBag.Mensaje = "Debe ingresar Razón social";
                }
                


            }


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


        //[HttpGet]
        //public async Task<IActionResult> Editar(int idSocio, string tipoSocioOrigen, string controladorOrigen, string accionOrigen)
        //{
        //    var datos = (await ETL_SociosComerciales()).FirstOrDefault(x => x.IdSocio == idSocio); 
        //    return View("Editar", datos);
        //}

        //#region Edit => HttpPost 

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Editar(VMSociosProveedores modelo)
        //{
        //    var keyRUC = (await _srvTipoDcmtoIdentidad.GetById(x => x.Simbolo == KeysNames.TIPO_DCMTO_IDENTIDAD_RUC)).IdTipoDcmtoIdentidad;
        //    var socio = await _srvSocio.GetById(x => x.IdSocio == modelo.IdSocio);

        //    socio.IdTipoSocio = modelo.IdTipoSocio;
        //    socio.IdTipoDcmtoIdentidad = modelo.IdTipoDcmtoIdentidadAsignado;
        //    socio.NumeroDcmtoIdentidad = modelo.NumeroDcmtoIdentidad;

        //    socio.RazonSocial = keyRUC == modelo.IdTipoDcmtoIdentidadAsignado ? modelo.RazonSocial : string.Empty;
        //    socio.ApellidoPaterno = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.ApellidoPaterno : string.Empty;
        //    socio.ApellidoMaterno = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.ApellidoMaterno : string.Empty;
        //    socio.PrimerNombre = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.PrimerNombre : string.Empty;
        //    socio.SegundoNombre = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.SegundoNombre : string.Empty;

        //    socio.Telefono = modelo.Telefono;
        //    socio.Celular = modelo.Celular;
        //    socio.Email = modelo.Email;
        //    socio.PaginaWeb = modelo.PaginaWeb;

        //    socio.IsAfectoRetencion = modelo.IsAfectoRetencion;
        //    socio.IsAfectoPercepcion = modelo.IsAfectoPercepcion;
        //    socio.IsBuenContribuyente = modelo.IsBuenContribuyente;
        //    socio.IdTipoCalificacion = modelo.IdTipoCalificacion;
        //    socio.ZonaPostal = modelo.ZonaPostal;
        //    socio.FechaInicioOperaciones = modelo.FechaInicioOperaciones.HasValue ? modelo.FechaInicioOperaciones.Value : null;

        //    socio.IdGrupoSocioNegocio = modelo.IdGrupoSocioNegocio.HasValue ? modelo.IdGrupoSocioNegocio : null;
        //    socio.IdColaboradorAsignado = modelo.IdColaboradorAsignado.HasValue ? modelo.IdColaboradorAsignado : null;

        //    socio.EsActivo = modelo.EsActivo;
        //    socio.UsuarioName = _sessionUsuario.Nombre;
        //    socio.FechaRegistro = DateTime.Now;

        //    bool flgRetorno = await _srvSocio.Update(socio);
        //    return RedirectToAction("Index");
        //}

        //#endregion

        //#region Eliminar => HttpGet

        //[HttpGet] 
        //public async Task<IActionResult> Eliminar(int idSocio, string tipoSocioOrigen, string controladorOrigen, string accionOrigen)
        //{
        //    return View("Eliminar");
        //}

        //#endregion

        //#region Eliminar => HttpPost

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Eliminar(VMSociosProveedores modelo)
        //{
        //    bool flgRetorno = await _srvSocio.Delete(x => x.IdSocio == modelo.IdSocio);
        //    return RedirectToAction("Index");
        //}

        //#endregion

        //#region Método  => Listar PDF   

        //public async Task<IActionResult> ListarPDF()
        //{
        //    var query = await ETL_SociosComerciales();

        //    string titulo = "Proveedores";
        //    string protocolo = Request.IsHttps ? "Https" : "Http";
        //    string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
        //    string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

        //    return new ViewAsPdf("ListarPDF", query)
        //    {
        //        FileName = $"Proveedores {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
        //        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
        //        PageSize = Rotativa.AspNetCore.Options.Size.A4,
        //        PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
        //        CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
        //    };
        //}

        //#endregion

        #region ETL =>  Proveedores

        public async Task<List<VMSociosProveedores>> ETL_SociosComerciales()
        { 
            var svtTD = new ServicioTiposDeDocumentos(_srvTipoDcmtoIdentidad);
            var tipoDocumentoIdentidad = await svtTD.SrvTiposDeDocumentos();

            var tipoSocioGruposEconomicos = await _contexto.TipoSocio.AsNoTracking().FirstOrDefaultAsync(x => x.Codigo == KeysNames.GRUPOS_ECONOMICOS);
            var tipoSocioColaboradores = await _contexto.TipoSocio.AsNoTracking().FirstOrDefaultAsync(x => x.Codigo == KeysNames.COLABORADORES);
            var tipoSocioProveedores = await _contexto.TipoSocio.AsNoTracking().FirstOrDefaultAsync(x => x.Codigo == KeysNames.PROVEEDORES);
                         
            var entityGruposEconomicos = _mapper.Map<List<VMSociosGrupos>>(await _contexto.Socio.AsNoTracking().Where(x => x.IdTipoSocio == tipoSocioGruposEconomicos.IdTipoSocio).ToListAsync());
            var entityColaboradores = _mapper.Map<List<VMSociosColaboradores>>(await _contexto.Socio.AsNoTracking().Where(x => x.IdTipoSocio == tipoSocioColaboradores.IdTipoSocio).ToListAsync());
            var entityProveedores = _mapper.Map<List<VMSociosProveedores>>(await _contexto.Socio.AsNoTracking().Where(x => x.IdTipoSocio == tipoSocioProveedores.IdTipoSocio).ToListAsync());

            var entityTipoCalificacion = _mapper.Map<List<VMTipoCalificacion>>(await _contexto.TipoCalificacion.ToListAsync());
             
            foreach (var item in entityTipoCalificacion)
            {
                item.CodigoAndDescripcion = item.Codigo + " - " + item.Descripcion;
            }

            foreach (var item in entityColaboradores)
            {
                item.ApellidosAndNombres = item.ApellidoPaterno + " " + item.ApellidoMaterno + " " + item.PrimerNombre + " " + item.SegundoNombre;
            }

            foreach (var item in entityProveedores)
            {
                item.IdTipoDcmtoIdentidadAsignado = item.IdTipoDcmtoIdentidad;
                item.CodigoTipoDcmto = tipoDocumentoIdentidad.FirstOrDefault(x => x.IdTipoDcmtoIdentidad == item.IdTipoDcmtoIdentidad).Simbolo;
                item.NameTipoDcmto = tipoDocumentoIdentidad.FirstOrDefault(x => x.IdTipoDcmtoIdentidad == item.IdTipoDcmtoIdentidad).Descripcion;
                item.TiposDcmtoIdentidad = tipoDocumentoIdentidad;
                item.NombreRazonSocial = item.CodigoTipoDcmto == KeysNames.TIPO_DCMTO_IDENTIDAD_RUC ? item.RazonSocial : item.ApellidoPaterno + " " + item.ApellidoMaterno + "" + item.PrimerNombre + "" + item.SegundoNombre;
                item.nav_GrupoEconomico = entityGruposEconomicos;
                item.nav_Colaboradores = entityColaboradores;
                item.nav_Calificacion = entityTipoCalificacion; 
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


        //#region Eliminar => HttpPost   
        ////<a class="btn btn-secondary btn-sm" href="@Url.Action("Eliminar", "Contactos", new { idSocio = item.IdSocio, tipoSocioOrigen=@tipoSocioOrigen, controladorOrigen=@controladorOrigen, accionOrigen=@accionOrigen})"><i class="bi bi-justify"></i>Contactos</a>


         
        //public async Task<IActionResult> EliminarProveedor(VMSociosProveedores modelo)
        //{
        //    var keyRUC = (await _srvTipoDcmtoIdentidad.GetById(x => x.Simbolo == KeysNames.TIPO_DCMTO_IDENTIDAD_RUC)).IdTipoDcmtoIdentidad;
        //    var socio = await _srvSocio.GetById(x => x.IdSocio == modelo.IdSocio);

        //    socio.IdTipoSocio = modelo.IdTipoSocio;
        //    socio.IdTipoDcmtoIdentidad = modelo.IdTipoDcmtoIdentidadAsignado;
        //    socio.NumeroDcmtoIdentidad = modelo.NumeroDcmtoIdentidad;

        //    socio.RazonSocial = keyRUC == modelo.IdTipoDcmtoIdentidadAsignado ? modelo.RazonSocial : string.Empty;
        //    socio.ApellidoPaterno = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.ApellidoPaterno : string.Empty;
        //    socio.ApellidoMaterno = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.ApellidoMaterno : string.Empty;
        //    socio.PrimerNombre = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.PrimerNombre : string.Empty;
        //    socio.SegundoNombre = keyRUC != modelo.IdTipoDcmtoIdentidadAsignado ? modelo.SegundoNombre : string.Empty;

        //    socio.Telefono = modelo.Telefono;
        //    socio.Celular = modelo.Celular;
        //    socio.Email = modelo.Email;
        //    socio.PaginaWeb = modelo.PaginaWeb;

        //    socio.IsAfectoRetencion = modelo.IsAfectoRetencion;
        //    socio.IsAfectoPercepcion = modelo.IsAfectoPercepcion;
        //    socio.IsBuenContribuyente = modelo.IsBuenContribuyente;
        //    socio.IdTipoCalificacion = modelo.IdTipoCalificacion;
        //    socio.ZonaPostal = modelo.ZonaPostal;
        //    socio.FechaInicioOperaciones = modelo.FechaInicioOperaciones.HasValue ? modelo.FechaInicioOperaciones.Value : null;

        //    socio.IdGrupoSocioNegocio = modelo.IdGrupoSocioNegocio.HasValue ? modelo.IdGrupoSocioNegocio : null;
        //    socio.IdColaboradorAsignado = modelo.IdColaboradorAsignado.HasValue ? modelo.IdColaboradorAsignado : null;

        //    socio.EsActivo = modelo.EsActivo;
        //    socio.UsuarioName = _sessionUsuario.Nombre;
        //    socio.FechaRegistro = DateTime.Now;

        //    bool flgRetorno = await _srvSocio.Update(socio);
        //    return View(socio);
        //}

            
        ////[HttpPost]      
        ////public async Task<IActionResult> xEliminarProveedor(VMSociosProveedores modelo)
        ////{ 
        ////    return RedirectToAction("EliminarProveedor", "SocioProveedores", modelo);
        ////}

        //#endregion


    }
}