namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMTipoFormaPago
    {
        public int IdTipoFormaPago { get; set; } 
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int DiasDePago { get; set; }
        public bool EsActivo { get; set; }
        
        public bool SeleccionInicial { get; set; }

        public bool SEL { get; set; } 
    }
}