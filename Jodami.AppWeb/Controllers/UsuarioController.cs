using Jodami.DAL.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;
using Jodami.Entity;
using Jodami.BLL.Interfaces;
using AutoMapper;
using Jodami.AppWeb.Models.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Jodami.AppWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly DbJodamiContext _contexto;
        private readonly IGenericService<Usuario> _service;

        #region Constructor 

        public UsuarioController(IMapper mapper, DbJodamiContext contexto, IGenericService<Usuario> service)
        {
            _mapper = mapper;
            _contexto = contexto;
            _service = service;
        }

        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //return View((await _service.GetAll()).ToList());

            return View();
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


        [HttpPost]
        public async Task<IActionResult> Crear(VMUsuarios viewModel)
        {
            return RedirectToAction("Index", "Home");
        }

        #region HttpGet => Lista de usuarios - Invocado por AJAX desde el  usuario_index.js

        [HttpGet]
        public async Task<IActionResult> ListaUsuarios()
        {
            var modelo = await _contexto.Usuario
                                .AsNoTracking()
                                .Include(e => e.IdRolNavigation)
                                .ToListAsync();

            var datos = new List<VMUsuarios>();

            foreach (var item in modelo.OrderBy(x => x.Nombre).ToList())
            {
                var obj = new VMUsuarios()
                {
                    IdUsuario = item.IdUsuario,
                    Nombres = item.Nombre,
                    Correo = item.Correo,
                    Telefono = item.Telefono,
                    IdRol = item.IdRol.Value,
                    UrlFoto = item.UrlFoto != null ? item.UrlFoto : string.Empty,
                    NombreFoto = item.NombreFoto != null ? item.NombreFoto : string.Empty,
                    Clave = item.Clave,
                    EsActivo = item.EsActivo.HasValue ? item.EsActivo.Value : false,
                    NameRolAsignado = item.IdRolNavigation.Descripcion
                };

                datos.Add(obj);
            }

            return StatusCode(StatusCodes.Status200OK, new { data = datos });
        }

        #endregion

        //#region HttpGet => Lista de roles    

        //[HttpGet]
        //public async Task<IActionResult> ListaRoles()
        //{
        //    var modelo = await _contexto.Rol.AsNoTracking().ToListAsync();
        //    var datos = new List<VMCargaDataCombos>();

        //    foreach (var item in modelo.OrderBy(x => x.Descripcion).ToList())
        //    {
        //        datos.Add(new VMCargaDataCombos()
        //        {
        //            codigoKey = item.IdRol.ToString(),
        //            nameKey = item.Descripcion
        //        });
        //    }

        //    return StatusCode(StatusCodes.Status200OK, new { responseJson = datos });
        //}

        //#endregion

        #region HttpGet => Lista de roles    

        //[HttpGet]
        public JsonResult ListaRoles()
        {
            var modelo = _contexto.Rol.AsNoTracking().ToList();
            var data = new List<VMCargaDataCombos>();

            foreach (var item in modelo.OrderBy(x => x.Descripcion).ToList())
            {
                data.Add(new VMCargaDataCombos()
                {
                    codigoKey = item.IdRol.ToString(),
                    nameKey = item.Descripcion
                });
            }
            return Json(data);
            //return StatusCode(StatusCodes.Status200OK, new { data = datos });
        }

        #endregion


    }
}