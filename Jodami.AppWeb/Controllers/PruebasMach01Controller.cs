using Jodami.AppWeb.Models;
using Jodami.BLL.Interfaces;
using Jodami.DAL.DBContext;
using Jodami.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Jodami.AppWeb.Controllers
{
    public class PruebasMach01Controller : Controller
    {
        private readonly DbJodamiContext _contexto;
        private readonly IHttpContextAccessor _httpContextAccessor; 

        #region Constructor 

        public PruebasMach01Controller(DbJodamiContext contexto, IGenericService<SocioContacto> svrSocioContacto, IHttpContextAccessor httpContextAccessor)
        {
            _contexto = contexto; 
            _httpContextAccessor = httpContextAccessor; 
        }

        #endregion


        // https://cdnjs.com/libraries/jquery

        // https://www.google.com/search?q=como+usar+AJAX+con+c%23&rlz=1C1CHBF_esPE1059PE1059&oq=como+usar+AJAX+con+c%23&gs_lcrp=EgZjaHJvbWUyBggAEEUYOTIHCAEQIRigAdIBCTc0ODlqMGoxNagCCLACAQ&sourceid=chrome&ie=UTF-8#fpstate=ive&ip=1&vld=cid:c1a82f1d,vid:3MxClIzj-9U,st:0


        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var modelo = new PruebasMach01();
            return View(modelo);
        }
         
        [HttpPost]
        public JsonResult Crear(string rut, string nombres, string apellidos, string clave)
        {  
            return Json("registrado");
        }

        [HttpGet]
        public async Task<IActionResult> Registro()
        { 
            return View();
        }

    }
}