using AutoMapper;
using Jodami.AppWeb.Models.ViewModels;
using Jodami.Entity;

namespace Jodami.AppWeb.Utilidades.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Moneda, VMMoneda>().ReverseMap();
            CreateMap<UnidadMedida, VMUnidadMedida>().ReverseMap();


        }        
    }
}
