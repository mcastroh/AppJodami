using Jodami.AppWeb.Models.ViewModels;
using Jodami.BLL.Interfaces;
using Jodami.Entity;

namespace Jodami.AppWeb.Utilidades.Servicios
{
    public class ServicioTiposDeDocumentos
    {
        private readonly IGenericService<TipoDocumentoIdentidad> _srvTipoDcmtoIdentidad;

        public ServicioTiposDeDocumentos(IGenericService<TipoDocumentoIdentidad> srvTipoDcmtoIdentidad)
        {
            _srvTipoDcmtoIdentidad = srvTipoDcmtoIdentidad;
        }

        public async Task<List<VMTipoDocumentoIdentidad>> SrvTiposDeDocumentos()
        {
            var tiposDcmtos = new List<VMTipoDocumentoIdentidad>();
            var datos = await _srvTipoDcmtoIdentidad.GetAll();

            foreach (var item in datos)
            {
                var obj = new VMTipoDocumentoIdentidad()
                {                    
                    IdTipoDcmtoIdentidad = item.IdTipoDcmtoIdentidad,
                    Descripcion = item.Descripcion,
                    Simbolo = item.Simbolo,
                    IdCodigoSunat = item.IdCodigoSunat,
                    SimboloAndDescripcion = item.Simbolo + " - " + item.Descripcion
                };

                tiposDcmtos.Add(obj);
            }

            return tiposDcmtos;
        }


    }
}
