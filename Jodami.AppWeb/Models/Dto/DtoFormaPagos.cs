//using Jodami.AppWeb.Models.ViewModels;
//using Jodami.Entity;

using Jodami.AppWeb.Models.ViewModels;

namespace Jodami.AppWeb.Models.Dto
{
    public class DtoFormaPagos
    {
        public int IdSocio { get; set; }
        public string TipoNroDcmto { get; set; }
        public string Nombres { get; set; }
        public string Situacion { get; set; }

        public string TipoSocio { get; set; }
        public string ControladorOrigen { get; set; }
        public string AccionOrigen { get; set; }

        //public SocioFormaPago NewSocioFormaPago { get; set; }
        //public List<SocioFormaPago> LstSocioFormaPago { get; set; }
        //public List<VMTipoFormaPago> LstTipoFormaPago { get; set; }

        public List<VMTipoFormaPago> vmTipoFormaPago { get; set; }
        
    }
}