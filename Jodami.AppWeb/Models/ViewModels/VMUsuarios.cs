namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMUsuarios
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Correo { get; set; } 
        public string Telefono { get; set; } 
        public int IdRol { get; set; } 
        public string UrlFoto { get; set; } 
        public string NombreFoto { get; set; } 
        public string Clave { get; set; } 
        public bool EsActivo { get; set; }

        public string NameRolAsignado { get; set; }
    }
}