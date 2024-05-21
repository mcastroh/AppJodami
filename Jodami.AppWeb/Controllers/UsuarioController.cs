using Jodami.DAL.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;  
using Jodami.Entity;
using Jodami.BLL.Interfaces;
using AutoMapper;
using Jodami.AppWeb.Models.ViewModels; 
using Jodami.AppWeb.Models;
using Newtonsoft.Json;
using Jodami.AppWeb.Utilidades.Servicios;
using System.Numerics;


namespace Jodami.AppWeb.Controllers
{
    public class UsuarioController : Controller
    { 
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DbJodamiContext _contexto;
        private readonly IGenericService<Usuario> _srvUsuario;

        #region Constructor 

        public UsuarioController(IMapper mapper, IHttpContextAccessor httpContextAccessor, DbJodamiContext contexto, IGenericService<Usuario> srvUsuario)
        {
            _contexto = contexto;
            _srvUsuario = srvUsuario;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion 

        #region HttpGet => Index  

        [HttpGet]
        public async Task<IActionResult> Index()
        { 
            return View();
        }

        #endregion

        #region HttpGet => Login

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

        #endregion

        #region HttpPost => Login

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

            var modelo = await _srvUsuario.GetById(x => x.Correo == usuario.Correo);

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

            var respuesta = ServicioSesionUsuario.Srv_SesionUserLogin_Set(_httpContextAccessor, HttpContext, modelo);
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region HttpPost => CRUD Insert

        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] IFormFile foto, [FromForm] string modelo)
        {
            var gResponse = new GenericResponse<VMUsuarios>();

            try
            {
                var vmUsuario = JsonConvert.DeserializeObject<VMUsuarios>(modelo);
                string fotoName = "";
                Stream fotoStream = null;

                if (foto != null)
                {
                    string nombreCodificado = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(foto.FileName);
                    fotoName = string.Concat(nombreCodificado, extension);
                    fotoStream = foto.OpenReadStream();
                }

                var entidad = new Usuario()
                {
                    Nombre = vmUsuario.Nombres,
                    Correo = vmUsuario.Correo,
                    Telefono = vmUsuario.Telefono,
                    IdRol = vmUsuario.IdRol,
                    UrlFoto = fotoStream != null ? fotoStream.ToString() : string.Empty,
                    NombreFoto = fotoName,
                    Clave = "1234",
                    EsActivo = true,
                    UsuarioName = ServicioSesionUsuario.Srv_SesionUserLogin_Get(_httpContextAccessor).Nombre,
                    FechaRegistro = DateTime.Now
                };

                var entity = await _srvUsuario.Insert(entidad);

                var objUsuario = new VMUsuarios();

                if (entity != null)
                {
                    var objRolUsuario = await _contexto.Rol.FirstAsync(x => x.IdRol == entity.IdRol);

                    objUsuario = new VMUsuarios()
                    {
                        IdUsuario = entity.IdUsuario,
                        Nombres = entity.Nombre,
                        Correo = entity.Correo,
                        Telefono = entity.Telefono,
                        IdRol = entity.IdRol.Value,
                        UrlFoto = entity.UrlFoto,
                        NombreFoto = entity.NombreFoto,
                        Clave = entity.Clave,
                        EsActivo = entity.EsActivo.Value ? 1 : 0,
                        NameRolAsignado = entity.IdRolNavigation.Descripcion
                    };

                    gResponse.Estado = true;
                    gResponse.Objeto = objUsuario;
                }
                else
                {
                    gResponse.Estado = false;
                    gResponse.Mensaje = "Usuario no pudo ser adicionado.";
                    gResponse.Objeto = null;
                }
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        #endregion 

         

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
                    EsActivo = item.EsActivo.HasValue ? 1 : 0,
                    NameRolAsignado = item.IdRolNavigation.Descripcion
                };

                datos.Add(obj);
            }

            return StatusCode(StatusCodes.Status200OK, new { data = datos });
        }

        #endregion

        #region HttpGet => Lista de roles    

        [HttpGet]
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
        }

        #endregion

    }
}