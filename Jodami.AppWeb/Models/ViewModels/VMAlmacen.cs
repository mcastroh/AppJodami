namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMAlmacen
    {
        public int IdAlmacen { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int? IdLocal { get; set; }
        public int IdTipoAlmacen { get; set; }
        public int? IdDireccion { get; set; }
        public int? IdResponsable { get; set; }
        public decimal Superficie { get; set; }
        public decimal Capacidad { get; set; }
        public bool EsActivo { get; set; }
    }
}