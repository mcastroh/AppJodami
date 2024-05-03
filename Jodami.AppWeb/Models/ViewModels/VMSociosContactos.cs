namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMSociosContactos
    {
        public int IdContacto { get; set; }
        public int IdSocio { get; set; }
        public string Nombre { get; set; }
        public int? IdCargo { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public bool IsCelularWhatsApp { get; set; }
        public string Email { get; set; }
        public bool EsActivo { get; set; }

        public VMCargos ObjCargo { get; set; }
    }
}