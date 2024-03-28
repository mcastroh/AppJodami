using Jodami.DAL.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jodami.AppWeb.Controllers
{
    public class MonedaController : Controller
    {
        private readonly DbJodamiContext _contexto;

        #region Constructor 

        public MonedaController(DbJodamiContext contexto)
        {
            _contexto = contexto;            
        }

        #endregion 

        [HttpGet]
        public async Task<IActionResult> Index()
        {            
            return View((await _contexto.Moneda.AsNoTracking().ToListAsync()).OrderBy(x => x.Simbolo).ToList());
        }


    }
}
