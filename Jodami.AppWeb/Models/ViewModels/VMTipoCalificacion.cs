namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMTipoCalificacion
    {
        public int IdTipoCalificacion { get; set; } 
        public string Codigo { get; set; } 
        public string Descripcion { get; set; } 
        public bool EsActivo { get; set; }

        public string CodigoAndDescripcion { get; set; }
        
    }
}