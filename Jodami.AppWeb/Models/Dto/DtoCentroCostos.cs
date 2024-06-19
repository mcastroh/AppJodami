using Jodami.AppWeb.Models.ViewModels;
using Jodami.Entity;

namespace Jodami.AppWeb.Models.Dto
{
    public class DtoCentroCostos
    {
        public DtoRespuesta DtoRespuesta { get; set; }

        public VMCentroCostos CentroCostoFundo { get; set; }
        public VMCentroCostos CentroCostoCultivo { get; set; }
        public VMCentroCostos CentroCostoVariedad { get; set; }
        public VMCentroCostos CentroCostoCampo { get; set; }

        public List<VMCentroCostos> CentroCostoNavigation { get; set; }
        //public List<UnidadGasto> UnidadGastoNavigation { get; set; }        

    }
}