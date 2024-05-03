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

            //CreateMap<Socio, VMSocios>().ReverseMap(); 
            CreateMap<Socio, VMSociosGrupos>().ReverseMap();
            CreateMap<Socio, VMSociosProveedores>().ReverseMap();
            CreateMap<Socio, VMSociosColaboradores>().ReverseMap();
            CreateMap<SocioContacto, VMSociosContactos>().ReverseMap();
            //CreateMap<SocioContacto, VMSociosContactos>().ReverseMap();


            // Artículos
            CreateMap<Almacen, VMAlmacen>().ReverseMap();
            CreateMap<TipoArticulo, VMTipoArticulo>().ReverseMap();
            CreateMap<UnidadMedida, VMUnidadMedida>().ReverseMap();
            CreateMap<TipoAlmacen, VMTipoAlmacen>().ReverseMap();
            CreateMap<SunatTipoDetraccion, VMSunatTipoDetraccion>().ReverseMap();
            CreateMap<SunatTipoExistencia, VMSunatTipoExistencia>().ReverseMap();
            CreateMap<GrupoArticulo, VMGrupoArticulo>().ReverseMap();
            CreateMap<SubGrupoArticulo, VMSubGrupoArticulo>().ReverseMap();
            CreateMap<TipoCuentaMayor, VMTipoCuentaMayor>().ReverseMap();
            CreateMap<TipoValorizacion, VMTipoValorizacion>().ReverseMap();
            CreateMap<Articulo, VMArticulos>().ReverseMap();


            // Ubigeos
            CreateMap<Departamento, VMUbigeoDepartamento>().ReverseMap();
            CreateMap<Provincia, VMUbigeoProvincia>().ReverseMap();
            CreateMap<Distrito, VMUbigeoDistrito>().ReverseMap();

            CreateMap<Cargo, VMCargos>().ReverseMap();


        }        
    }
}
