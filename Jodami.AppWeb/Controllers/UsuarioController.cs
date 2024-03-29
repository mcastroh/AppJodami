using Jodami.DAL.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jodami.AppWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly DbJodamiContext _contexto;

        #region Constructor 

        public UsuarioController(DbJodamiContext contexto)
        {
            _contexto = contexto;            
        }

        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index()
        {            
            return View((await _contexto.Usuario.AsNoTracking().ToListAsync()).OrderBy(x => x.Nombre).ToList());
        }
        
        #endregion

    }
}
