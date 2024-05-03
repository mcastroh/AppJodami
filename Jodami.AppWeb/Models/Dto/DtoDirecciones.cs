using Jodami.Entity;

namespace Jodami.AppWeb.Models.Dto
{
    public class DtoDirecciones
    {
        public int IdSocio { get; set; }
        public string TipoNroDcmto { get; set; }
        public string Nombres { get; set; }
        public string Situacion { get; set; }

        public string TipoSocio { get; set; }
        public string ControladorOrigen { get; set; }
        public string AccionOrigen { get; set; }



        public SocioDireccion NewSocioDireccion { get; set; }
        public Direccion NewDireccion { get; set; }

        public List<SocioDireccion> LstSocioDirecciones { get; set; }
        public List<TipoDireccion> LstTipoDirecciones { get; set; }
        public List<TipoVia> LstTipoVias { get; set; }

        public List<TipoZona> LstTipoZonas { get; set; }

        public string DepartamentoKey { get; set; }
        public string ProvinciaKey { get; set; }
        public string DistritoKey { get; set; }

    }
}