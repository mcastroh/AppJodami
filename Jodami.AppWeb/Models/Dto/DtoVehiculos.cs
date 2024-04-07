using Jodami.AppWeb.Models.ViewModels;

namespace Jodami.AppWeb.Models.Dto
{
    public class DtoVehiculos
    {
        public VMVehiculos objVehiculos { get; set; }
        public List<VMVehiculos> listaVehiculos { get; set; }
        public List<VMTipoFlete> listaTipoFlete { get; set; }
    }
}