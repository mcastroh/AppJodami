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
    public class SocioComercialGruposController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<Socio> _srvSocio;
        private readonly IGenericService<TipoSocio> _srvTipoSocio;
        private readonly IGenericService<TipoDocumentoIdentidad> _srvTipoDcmtoIdentidad;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Usuario _sessionUsuario;

        #region Constructor 

        public SocioComercialGruposController(IMapper mapper, IGenericService<Socio> srvSocio, IGenericService<TipoSocio> srvTipoSocio, IGenericService<TipoDocumentoIdentidad> srvTipoDcmtoIdentidad, IHttpContextAccessor httpContextAccessor)
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
            var tipoSocio = await _srvTipoSocio.GetById(x => x.Codigo == KeysNames.GRUPOS_ECONOMICOS);
            var query = _mapper.Map<List<VMSocios>>(await _srvSocio.GetByFilter(x => x.IdTipoSocio == tipoSocio.IdTipoSocio));

            ServicioTiposDeDocumentos svtTD = new ServicioTiposDeDocumentos(_srvTipoDcmtoIdentidad);
            var tipoDocumentoIdentidad = await svtTD.SrvTiposDeDocumentos();

            ViewBag.TipoDocumentoIdentidad = tipoDocumentoIdentidad.ToList();
            ViewBag.TipoSocio = tipoSocio;

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
            var tipoSocio = await _srvTipoSocio.GetById(x => x.Codigo == KeysNames.GRUPOS_ECONOMICOS);
            var query = _mapper.Map<List<VMSocios>>(await _srvSocio.GetByFilter(x => x.IdTipoSocio == tipoSocio.IdTipoSocio));

            string titulo = tipoSocio.Descripcion;
            string protocolo = Request.IsHttps ? "Https" : "Http";
            string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
            string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

            return new ViewAsPdf("ListarPDF", query)
            {
                FileName = $"{titulo} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
                CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
            };
        }

        #endregion

    }
}