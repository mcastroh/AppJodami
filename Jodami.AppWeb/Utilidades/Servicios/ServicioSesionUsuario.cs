using Jodami.AppWeb.Models.Dto;
using Jodami.Entity;
using System.Text.Json;

namespace Jodami.AppWeb.Utilidades.Servicios
{
    public static class ServicioSesionUsuario
    {
        #region Graba Sesión del Usuario Login

        public static DtoRespuesta Srv_SesionUserLogin_Set(IHttpContextAccessor _httpContextAccessor, HttpContext httpContext, Usuario modelo)
        {
            var respuesta = new DtoRespuesta();

            try
            {
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
                    WriteIndented = true,
                };

                httpContext.Session.SetString("sessionUsuario", JsonSerializer.Serialize(modelo, options));
                respuesta.Estado = true;
                 
            }
            catch (Exception ex)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = ex.Message;
            }

            return respuesta;
        }

        #endregion

        #region Recupera Sesión del Usuario Login

        public static Usuario Srv_SesionUserLogin_Get(IHttpContextAccessor _httpContextAccessor)
        {
            return JsonSerializer.Deserialize<Usuario>(_httpContextAccessor.HttpContext.Session.GetString("sessionUsuario"));
        }

        #endregion

    }
}