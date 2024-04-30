namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMSunatTipoDetraccion
    {
        public int IdTipoDetraccion { get; set; }
        public string Codigo { get; set; }   
        public string Descripcion { get; set; }
        public decimal Porcentaje { get; set; }
        public string Condicion { get; set; }
        public decimal Valor { get; set; }
        public string Unidad { get; set; }
        public bool Activo { get; set; }
    }
}