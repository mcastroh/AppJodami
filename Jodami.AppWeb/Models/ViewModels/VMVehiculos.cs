using Jodami.Entity;

namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMVehiculos
    {
        public int IdVehiculo { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public string Placa { get; set; }
        public string Certificado { get; set; }
        public decimal? PesoKg { get; set; }
        public bool EsDeEmpresa { get; set; }
        public int? IdFlete { get; set; }
        public bool EsActivo { get; set; }

        public virtual TipoFlete IdFleteNavigation { get; set; }


        public string CodigoFlete { get; set; }
        public List<VMTipoFlete> TiposDeFlete { get; set; }

    }
}