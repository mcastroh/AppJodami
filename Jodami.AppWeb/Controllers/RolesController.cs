using AutoMapper;
using Jodami.AppWeb.Models.ViewModels;
using Jodami.DAL.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jodami.AppWeb.Controllers
{
    public class RolesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly DbJodamiContext _contexto;       

        #region Constructor 

        public RolesController(IMapper mapper, DbJodamiContext contexto)
        { 
            _mapper = mapper;
            _contexto = contexto; 
        }

        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index()
        {    
            return View();
        }

        #endregion 


        #region HttpGet => Lista de roles   

        [HttpGet]
        public async Task<IActionResult> ListRoles()
        {
            var modelo = await _contexto.Rol.AsNoTracking().ToListAsync();

            var datos = new List<VMCargaDataCombos>();

            foreach (var item in modelo.OrderBy(x => x.Descripcion).ToList())
            {
                var obj = new VMCargaDataCombos()
                {
                    codigoKey = item.IdRol.ToString(),
                    nameKey = item.Descripcion  
                };

                datos.Add(obj);
            }

            return StatusCode(StatusCodes.Status200OK, new { data = datos });
        }

        #endregion
    }
}
