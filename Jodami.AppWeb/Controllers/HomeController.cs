using AutoMapper;
using Jodami.AppWeb.Models;
using Jodami.AppWeb.Models.ViewModels;
using Jodami.BLL.Interfaces;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Jodami.AppWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericService<Articulo> _srvArticulo;
        private readonly IMapper _mapper;

        #region Constructor 

        public HomeController(ILogger<HomeController> logger, IGenericService<Articulo> srvArticulo, IMapper mapper)
        {
            _logger = logger;
            _srvArticulo = srvArticulo;
            _mapper = mapper;
        }

        #endregion

        #region HttpGet => Index  

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        public IActionResult Perfil()
        {
            return View();
        }
         
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #region Rotativa => Cabecera

        public IActionResult Header(string titulo)
        {
            //string var = Request.QueryString.ToString();
            ViewBag.Titulo = titulo;
            return View("_Header");
        }

        #endregion 

        #region Rotativa => Pie de Página

        public IActionResult Footer(int page)
        {
            ViewBag.Pagina = page;
            return View("_Footer");
        }

        #endregion

        async public Task<ViewResult> ArticuloIndex()
        {
            return View();
        }

        async public Task<PartialViewResult> Articulo()
        {
            var articulos = _mapper.Map<List<VMArticulos>>(await _srvArticulo.GetAll());
            return PartialView(articulos);
        }

    }
}
