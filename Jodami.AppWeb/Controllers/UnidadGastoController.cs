using Jodami.BLL.Interfaces;
using Jodami.DAL.DBContext;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace Jodami.AppWeb.Controllers
{
    public class UnidadGastoController : Controller
    {
        #region Variables 

        private readonly Usuario _sessionUsuario;
        private readonly DbJodamiContext _contexto;
        private readonly IGenericService<UnidadGasto> _srvUnidadGasto;      
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor 

        public UnidadGastoController(DbJodamiContext contexto, IGenericService<UnidadGasto> srvUnidadGasto, IHttpContextAccessor httpContextAccessor)
        {
            _contexto = contexto;
            _srvUnidadGasto = srvUnidadGasto;
            _httpContextAccessor = httpContextAccessor;
            _sessionUsuario = System.Text.Json.JsonSerializer.Deserialize<Usuario>(_httpContextAccessor.HttpContext.Session.GetString("sessionUsuario"));
        }

        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var modelo = await _contexto.UnidadGasto.AsNoTracking().ToListAsync();
            return View(modelo);
        }

        #endregion

        #region Adicionar => HttpPost

        [HttpPost]
        public async Task<IActionResult> Adicionar(string descripcion)
        {
            var modelo = new UnidadGasto()
            {               
                Descripcion = descripcion,               
                EsActivo = true,
                UsuarioName = _sessionUsuario.Nombre,
                FechaRegistro = DateTime.Now
            };

            var entity = await _srvUnidadGasto.Insert(modelo);
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit => HttpPost 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(UnidadGasto entidad)
        {
            var modelo = await _srvUnidadGasto.GetById(x => x.IdUnidadGasto == entidad.IdUnidadGasto);             
            modelo.Descripcion = entidad.Descripcion;           
            modelo.EsActivo = entidad.EsActivo;
            modelo.UsuarioName = _sessionUsuario.Nombre;
            modelo.FechaRegistro = DateTime.Now;
            bool flgRetorno = await _srvUnidadGasto.Update(modelo);
            return RedirectToAction("Index");
        }

        #endregion

        #region Eliminar => HttpPost

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(UnidadGasto entidad)
        {
            bool flgRetorno = await _srvUnidadGasto.Delete(x => x.IdUnidadGasto == entidad.IdUnidadGasto);
            return RedirectToAction("Index");
        }

        #endregion

        #region Método  => Listar PDF   

        public async Task<IActionResult> ListarPDF()
        {
            var modelo = await _contexto.UnidadGasto.AsNoTracking().ToListAsync();

            string titulo = "Unidades de Gastos";
            string protocolo = Request.IsHttps ? "Https" : "Http";
            string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
            string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

            return new ViewAsPdf("ListarPDF", modelo)
            {
                FileName = $"Unidades de Gasto {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
                CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
            };
        }

        #endregion

    }
}
