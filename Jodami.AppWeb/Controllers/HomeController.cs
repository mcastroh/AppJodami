using Jodami.AppWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Jodami.AppWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        #region Constructor 

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

       

    }
}
