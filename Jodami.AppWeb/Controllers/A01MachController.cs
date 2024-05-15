using Jodami.BLL.Interfaces;
using Jodami.DAL.DBContext;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Jodami.AppWeb.Controllers
{
    public class A01MachController : Controller
    {
        private readonly DbJodamiContext _contexto;
        private readonly IHttpContextAccessor _httpContextAccessor; 

        #region Constructor 

        public A01MachController(DbJodamiContext contexto, IGenericService<SocioContacto> svrSocioContacto, IHttpContextAccessor httpContextAccessor)
        {
            _contexto = contexto; 
            _httpContextAccessor = httpContextAccessor; 
        }

        #endregion
         
        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> M01_VistaParcial_DesdeUnSelect()
        { 
            return View();
        }

        #endregion

       

    }
}