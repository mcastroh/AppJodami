using Jodami.DAL.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;
using Jodami.Entity;
using Jodami.BLL.Interfaces;
using AutoMapper;

namespace Jodami.AppWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<Usuario> _service;

        #region Constructor 

        public UsuarioController(IMapper mapper, IGenericService<Usuario> service)
        {
            _mapper = mapper;
            _service = service;
        }

        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index()
        {            
            return View((await _service.GetAll()).ToList());
        }

        #endregion



        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal claims = HttpContext.User;

            if (claims.Identity != null)
            {
                if (claims.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(new Usuario());
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario usuario)
        {
            if (usuario == null || (string.IsNullOrEmpty(usuario.Correo) == true && string.IsNullOrEmpty(usuario.Clave) == true))
            {
                ViewData["Mensaje"] = "Debe ingresar Usuario y Contraseña.";
                return View(usuario);
            }


            if (string.IsNullOrEmpty(usuario.Correo) == true)
            {
                ViewData["Mensaje"] = "Debe ingresar su email.";
                return View(usuario);
            }

            if (string.IsNullOrEmpty(usuario.Clave) == true)
            {
                ViewData["Mensaje"] = "Debe ingresar su clave.";
                return View(usuario);
            }

            var modelo = await _service.GetById(x => x.Correo == usuario.Correo); 

            if (modelo == null)
            {
                ViewData["Mensaje"] = $"Usuario no registrado en la Tabla de Usuarios.";
                return View(usuario);
            } 

            if (modelo.Clave != usuario.Clave)
            {
                ViewData["Mensaje"] = $"Contraseña ingresada {usuario.Clave} es incorrecta.";
                return View(usuario);
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true,
            };

            HttpContext.Session.SetString("sessionUsuario", JsonSerializer.Serialize(modelo, options));
            return RedirectToAction("Index", "Home");
        }
    }
}
