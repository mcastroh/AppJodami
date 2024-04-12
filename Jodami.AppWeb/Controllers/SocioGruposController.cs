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
    public class SocioGruposController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<Socio> _srvSocio;
        private readonly IGenericService<TipoSocio> _srvTipoSocio;
        private readonly IGenericService<TipoDocumentoIdentidad> _srvTipoDcmtoIdentidad;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Usuario _sessionUsuario;

        #region Constructor 

        public SocioGruposController(IMapper mapper, IGenericService<Socio> srvSocio, IGenericService<TipoSocio> srvTipoSocio, IGenericService<TipoDocumentoIdentidad> srvTipoDcmtoIdentidad, IHttpContextAccessor httpContextAccessor)
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
        public async Task<IActionResult> Adicionar(VMSocios modelo)
        {
            var socio = new Socio()
            {
                IdTipoSocio = modelo.IdTipoSocio,
                IdTipoDcmtoIdentidad = modelo.IdTipoDcmtoIdentidad,
                NumeroDcmtoIdentidad = modelo.NumeroDcmtoIdentidad,
                RazonSocial = modelo.RazonSocial,
                Telefono = modelo.Telefono,
                Celular = modelo.Celular,
                PaginaWeb = modelo.PaginaWeb,
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
        public async Task<IActionResult> Editar(VMSocios modelo)
        {
            var socio = await _srvSocio.GetById(x => x.IdSocio == modelo.IdSocio);

            socio.IdTipoSocio = modelo.IdTipoSocio;
            socio.IdTipoDcmtoIdentidad = modelo.IdTipoDcmtoIdentidad;
            socio.NumeroDcmtoIdentidad = modelo.NumeroDcmtoIdentidad;
            socio.RazonSocial = modelo.RazonSocial;
            socio.Telefono = modelo.Telefono;
            socio.Celular = modelo.Celular;
            socio.PaginaWeb = modelo.PaginaWeb;
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
        public async Task<IActionResult> Eliminar(VMSocios modelo)
        {
            bool flgRetorno = await _srvSocio.Delete(x => x.IdSocio == modelo.IdSocio);
            return RedirectToAction("Index");
        }

        #endregion

        #region Método  => Listar PDF   

        public async Task<IActionResult> ListarPDF()
        {
            var query = await ETL_SociosComerciales();

            string titulo = "Grupos Económicos";
            string protocolo = Request.IsHttps ? "Https" : "Http";
            string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
            string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

            return new ViewAsPdf("ListarPDF", query)
            {
                FileName = $"Grupos Economicos {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
                CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
            };
        }

        #endregion

        #region ETL =>  Grupos Económicos

        public async Task<List<VMSociosGrupos>> ETL_SociosComerciales()
        { 
            ServicioTiposDeDocumentos svtTD = new ServicioTiposDeDocumentos(_srvTipoDcmtoIdentidad);
            var tipoDocumentoIdentidad = await svtTD.SrvTiposDeDocumentos();

            var tipoSocio = await _srvTipoSocio.GetById(x => x.Codigo == KeysNames.GRUPOS_ECONOMICOS);  
            var grupo = _mapper.Map<List<VMSociosGrupos>>(await _srvSocio.GetByFilter(x => x.IdTipoSocio == tipoSocio.IdTipoSocio));

            foreach ( var item in grupo) 
            { 
                item.CodigoTipoDcmto = tipoDocumentoIdentidad.FirstOrDefault(x=> x.IdTipoDcmtoIdentidad == item.IdTipoDcmtoIdentidad).Simbolo;
                item.NameTipoDcmto = tipoDocumentoIdentidad.FirstOrDefault(x => x.IdTipoDcmtoIdentidad == item.IdTipoDcmtoIdentidad).Descripcion;
                item.TiposDcmtoIdentidad = tipoDocumentoIdentidad;
            }             

            ViewBag.TipoDocumentoIdentidad = tipoDocumentoIdentidad.ToList();
            ViewBag.TipoSocio = tipoSocio;
            ViewBag.DcmtoDefault = tipoDocumentoIdentidad.FirstOrDefault(x => x.Simbolo == KeysNames.TIPO_DCMTO_IDENTIDAD_OTR);

            return grupo;
        }

        #endregion 


    }
}