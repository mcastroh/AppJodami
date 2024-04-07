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
           
             
            CreateMap<TipoVia, VMTipoVia>().ReverseMap();
            CreateMap<TipoZona, VMTipoZona>().ReverseMap();

            CreateMap<TipoFlete, VMTipoFlete>().ReverseMap();
            CreateMap<Vehiculos, VMVehiculos>().ReverseMap();

             

            // Socios Comerciales
            CreateMap<TipoCalificacion, VMTipoCalificacion>().ReverseMap();
            CreateMap<TipoCuentaBancaria, VMTipoCuentaBancaria>().ReverseMap();
            CreateMap<TipoDireccion, VMTipoDireccion>().ReverseMap();
            CreateMap<TipoDocumentoIdentidad, VMTipoDocumentoIdentidad>().ReverseMap();

            // Artículos
            CreateMap<TipoArticulo, VMTipoArticulo>().ReverseMap();
            CreateMap<UnidadMedida, VMUnidadMedida>().ReverseMap();
            CreateMap<TipoAlmacen, VMTipoAlmacen>().ReverseMap();


            // Ubigeos
            CreateMap<Departamento, VMUbigeoDepartamento>().ReverseMap();
            CreateMap<Provincia, VMUbigeoProvincia>().ReverseMap();
            CreateMap<Distrito, VMUbigeoDistrito>().ReverseMap();



        }        
    }
}
