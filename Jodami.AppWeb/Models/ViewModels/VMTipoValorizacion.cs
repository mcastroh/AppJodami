namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMTipoValorizacion
    {
        public int IdTipoValorizacion { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public bool EsActivo { get; set; }
    }
}