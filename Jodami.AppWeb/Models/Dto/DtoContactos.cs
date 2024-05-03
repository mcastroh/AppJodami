using Jodami.Entity;

namespace Jodami.AppWeb.Models.Dto
{
    public class DtoContactos
    {
        public int IdSocio { get; set; }
        public string TipoNroDcmto { get; set; }
        public string Nombres { get; set; }
        public string Situacion { get; set; }

        public string TipoSocio { get; set; }
        public string ControladorOrigen { get; set; }
        public string AccionOrigen { get; set; }

        public SocioContacto NewContacto { get; set; }

        public List<SocioContacto> LstContactos { get; set; }
        public List<Cargo> LstCargos { get; set; }

    }
}