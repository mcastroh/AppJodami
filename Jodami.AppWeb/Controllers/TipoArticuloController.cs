using AutoMapper;
using Jodami.AppWeb.Models.ViewModels;
using Jodami.BLL.Interfaces;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace Jodami.AppWeb.Controllers
{
    public class TipoArticuloController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<TipoArticulo> _service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Usuario _sessionUsuario;

        #region Constructor 

        public TipoArticuloController(IMapper mapper, IGenericService<TipoArticulo> service, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            _sessionUsuario = System.Text.Json.JsonSerializer.Deserialize<Usuario>(_httpContextAccessor.HttpContext.Session.GetString("sessionUsuario"));
        }
        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var query = _mapper.Map<List<VMTipoArticulo>>(await _service.GetAll());
            return View(query);
        }

        #endregion

        #region Adicionar => HttpPost

        [HttpPost]
        public async Task<IActionResult> Adicionar(string descripcion)
        {
            var modelo = new TipoArticulo()
            {
                Descripcion = descripcion,                
                EsActivo = true,
                UsuarioName = _sessionUsuario.Nombre,
                FechaRegistro = DateTime.Now
            };

            var entity = await _service.Insert(modelo);
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit => HttpPost 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(VMTipoArticulo vmModelo)
        {
            var modelo = await _service.GetById(x => x.IdTipoArticulo == vmModelo.IdTipoArticulo);

            modelo.Descripcion = vmModelo.Descripcion;         
            modelo.EsActivo = vmModelo.EsActivo;
            modelo.UsuarioName = _sessionUsuario.Nombre;
            modelo.FechaRegistro = DateTime.Now;

            bool flgRetorno = await _service.Update(modelo);
            return RedirectToAction("Index");
        }

        #endregion

        #region Eliminar => HttpPost

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(VMTipoArticulo vmModelo)
        {
            bool flgRetorno = await _service.Delete(x => x.IdTipoArticulo == vmModelo.IdTipoArticulo);
            return RedirectToAction("Index");
        }

        #endregion

        #region Método  => Listar PDF   

        public async Task<IActionResult> ListarPDF()
        {
            var query = _mapper.Map<List<VMTipoArticulo>>(await _service.GetAll());

            string titulo = "Tipos de Artículo";
            string protocolo = Request.IsHttps ? "Https" : "Http";
            string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
            string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

            return new ViewAsPdf("ListarPDF", query)
            {
                FileName = $"Tipos de rticulo {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
                CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
            };
        }

        #endregion

    }
}
