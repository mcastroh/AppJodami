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
    public class SocioColaboradoresController : Controller
    {
        private readonly IMapper _mapper;

        private readonly IGenericService<Socio> _srvSocio;
        private readonly IGenericService<TipoSocio> _srvTipoSocio;
        private readonly IGenericService<TipoDocumentoIdentidad> _srvTipoDcmtoIdentidad;
       
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Usuario _sessionUsuario;

        #region Constructor 

        public SocioColaboradoresController(
            IMapper mapper, 
            IGenericService<Socio> srvSocio, 
            IGenericService<TipoSocio> srvTipoSocio, 
            IGenericService<TipoDocumentoIdentidad> srvTipoDcmtoIdentidad,           
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _srvSocio = srvSocio;
            _srvTipoSocio = srvTipoSocio;
            _srvTipoDcmtoIdentidad = srvTipoDcmtoIdentidad;           
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
        public async Task<IActionResult> Adicionar(VMSociosColaboradores modelo)
        {
            var socio = new Socio()
            {
                IdTipoSocio = modelo.IdTipoSocio,
                IdTipoDcmtoIdentidad = modelo.IdTipoDcmtoIdentidad,
                NumeroDcmtoIdentidad = modelo.NumeroDcmtoIdentidad,
                ApellidoPaterno = modelo.ApellidoPaterno,
                ApellidoMaterno = modelo.ApellidoMaterno,
                PrimerNombre = modelo.PrimerNombre,
                SegundoNombre = modelo.SegundoNombre, 
                Telefono = modelo.Telefono,
                Celular = modelo.Celular, 
                Email = modelo.Email,
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
        public async Task<IActionResult> Editar(VMSociosColaboradores modelo)
        {
            //var entity = (await _srvSocio.GetByFilter(x => x.IdTipoSocio != modelo.IdSocio && x.IdTipoDcmtoIdentidad == modelo.IdTipoDcmtoIdentidad && x.NumeroDcmtoIdentidad == modelo.NumeroDcmtoIdentidad)).ToList();

            //if (entity.Count != 0)
            //{
            //    return RedirectToAction("_AddPartialView", modelo);
            //}

            var socio = await _srvSocio.GetById(x => x.IdSocio == modelo.IdSocio);

            socio.IdTipoSocio = modelo.IdTipoSocio;
            socio.IdTipoDcmtoIdentidad = modelo.IdTipoDcmtoIdentidad;
            socio.NumeroDcmtoIdentidad = modelo.NumeroDcmtoIdentidad;
            socio.ApellidoPaterno = modelo.ApellidoPaterno;
            socio.ApellidoMaterno = modelo.ApellidoMaterno;
            socio.PrimerNombre = modelo.PrimerNombre;
            socio.SegundoNombre = modelo.SegundoNombre;
            socio.Telefono = modelo.Telefono;
            socio.Celular = modelo.Celular;
            socio.Email = modelo.Email;
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
        public async Task<IActionResult> Eliminar(VMSociosColaboradores modelo)
        {
            bool flgRetorno = await _srvSocio.Delete(x => x.IdSocio == modelo.IdSocio);
            return RedirectToAction("Index");
        }

        #endregion

        #region Método  => Listar PDF   

        public async Task<IActionResult> ListarPDF()
        {
            var query = await ETL_SociosComerciales();

            string titulo = "Colaboradores";
            string protocolo = Request.IsHttps ? "Https" : "Http";
            string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
            string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

            return new ViewAsPdf("ListarPDF", query)
            {
                FileName = $"Colaboradores {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
                CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
            };
        }

        #endregion

        #region ETL =>  Colaboradores

        public async Task<List<VMSociosColaboradores>> ETL_SociosComerciales()
        { 
            ServicioTiposDeDocumentos svtTD = new ServicioTiposDeDocumentos(_srvTipoDcmtoIdentidad);

            var tipoDocumentoIdentidad = await svtTD.SrvTiposDeDocumentos();     
            var tipoSocioColaboradores = await _srvTipoSocio.GetById(x => x.Codigo == KeysNames.COLABORADORES);     
          
            var entityColaboradores = _mapper.Map<List<VMSociosColaboradores>>(await _srvSocio.GetByFilter(x => x.IdTipoSocio == tipoSocioColaboradores.IdTipoSocio));
             
            foreach ( var item in entityColaboradores) 
            { 
                item.CodigoTipoDcmto = tipoDocumentoIdentidad.FirstOrDefault(x=> x.IdTipoDcmtoIdentidad == item.IdTipoDcmtoIdentidad).Simbolo;
                item.NameTipoDcmto = tipoDocumentoIdentidad.FirstOrDefault(x => x.IdTipoDcmtoIdentidad == item.IdTipoDcmtoIdentidad).Descripcion;
                item.ApellidosAndNombres = item.ApellidoPaterno + " " + item.ApellidoMaterno + " " + item.PrimerNombre + " " + item.SegundoNombre;
                item.TiposDcmtoIdentidad = tipoDocumentoIdentidad;
                
            }             

            ViewBag.TipoDocumentoIdentidad = tipoDocumentoIdentidad.ToList();
            ViewBag.TipoSocio = tipoSocioColaboradores;
            ViewBag.DcmtoDefault = tipoDocumentoIdentidad.FirstOrDefault(x => x.Simbolo == KeysNames.TIPO_DCMTO_IDENTIDAD_DNI); 

            return entityColaboradores;
        }

        #endregion 


    }
}