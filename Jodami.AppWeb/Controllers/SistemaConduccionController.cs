using Jodami.BLL.Interfaces;
using Jodami.DAL.DBContext;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace Jodami.AppWeb.Controllers
{
    public class SistemaConduccionController : Controller
    {
        #region Variables 

        private readonly Usuario _sessionUsuario;
        private readonly DbJodamiContext _contexto;
        private readonly IGenericService<NivelCentroCosto> _svrNivelCentroCosto;      
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor 

        public SistemaConduccionController(DbJodamiContext contexto, IGenericService<NivelCentroCosto> svrNivelCentroCosto, IHttpContextAccessor httpContextAccessor)
        {
            _contexto = contexto;
            _svrNivelCentroCosto = svrNivelCentroCosto;
            _httpContextAccessor = httpContextAccessor;
            _sessionUsuario = System.Text.Json.JsonSerializer.Deserialize<Usuario>(_httpContextAccessor.HttpContext.Session.GetString("sessionUsuario"));
        }

        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var modelo = await _contexto.NivelCentroCosto.AsNoTracking().ToListAsync();
            return View(modelo);
        }

        #endregion

        #region Adicionar => HttpPost

        [HttpPost]
        public async Task<IActionResult> Adicionar(string descripcion)
        {
            var modelo = new NivelCentroCosto()
            {               
                Descripcion = descripcion,               
                EsActivo = true,
                UsuarioName = _sessionUsuario.Nombre,
                FechaRegistro = DateTime.Now
            };

            var entity = await _svrNivelCentroCosto.Insert(modelo);
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit => HttpPost 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(NivelCentroCosto entidad)
        {
            var modelo = await _svrNivelCentroCosto.GetById(x => x.IdNivelCentroCosto == entidad.IdNivelCentroCosto);             
            modelo.Descripcion = entidad.Descripcion;           
            modelo.EsActivo = entidad.EsActivo;
            modelo.UsuarioName = _sessionUsuario.Nombre;
            modelo.FechaRegistro = DateTime.Now;
            bool flgRetorno = await _svrNivelCentroCosto.Update(modelo);
            return RedirectToAction("Index");
        }

        #endregion

        #region Eliminar => HttpPost

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(NivelCentroCosto entidad)
        {
            bool flgRetorno = await _svrNivelCentroCosto.Delete(x => x.IdNivelCentroCosto == entidad.IdNivelCentroCosto);
            return RedirectToAction("Index");
        }

        #endregion

        #region Método  => Listar PDF   

        public async Task<IActionResult> ListarPDF()
        {
            var modelo = await _contexto.NivelCentroCosto.AsNoTracking().ToListAsync();

            string titulo = "Niveles Centros de Costos";
            string protocolo = Request.IsHttps ? "Https" : "Http";
            string headerAction = Url.Action("Header", "Home", new { titulo }, protocolo);
            string footerAction = Url.Action("Footer", "Home", new { }, protocolo);

            return new ViewAsPdf("ListarPDF", modelo)
            {
                FileName = $"Niveles Centros de Costos {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 15, Bottom = 10, Right = 15, Top = 30 },
                CustomSwitches = $"--header-html {headerAction} --header-spacing 2 --footer-html {footerAction} --footer-spacing 2"
            };
        }

        #endregion

    }
}
