using Jodami.Entity;

namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMCentroCostos
    { 
        public int IdCentroCosto { get; set; } 
        public int IdNivelCentroCosto { get; set; }
        public string CodigoCentroCosto { get; set; }
        public string Descripcion { get; set; }
        public int? IdCentroCostoPadre { get; set; }
        public bool EsNivelEspecifico { get; set; }
        public bool EsAgricola { get; set; }
        public decimal Hectareas { get; set; }
        public decimal DistanciaEntreHilerasMetros { get; set; }
        public int OrdenAplicaFertilizantes { get; set; }        
        public string NumeroLote { get; set; }
        public int? IdUnidadGasto { get; set; }
        public int? IdSistemaConduccion { get; set; }
        public int? IdCultivo { get; set; }
        public bool EsActivo { get; set; }

        public UnidadGasto IdUnidadGastoNavigation { get; set; }
        public SistemaConduccion IdSistemaConduccionNavigation { get; set; }
        public Cultivo IdCultivoNavigation { get; set; }

        public List<UnidadGasto> LstUnidadGasto { get; set; }
        public List<SistemaConduccion> LstSistemaConduccion { get; set; }
        public List<Cultivo> LstCultivo { get; set; }


        //public UnidadGasto UnidadGastoNavigation { get; set; }
        //public SistemaConduccion SistemaConduccionNavigation { get; set; }
        //public Cultivo CultivoNavigation { get; set; }

        //[ForeignKey("IdCentroCostoPadre")]
        //[InverseProperty("InverseIdCentroCostoPadreNavigation")]
        //public virtual CentroCosto IdCentroCostoPadreNavigation { get; set; }

        //[ForeignKey("IdNivelCentroCosto")]
        //[InverseProperty("CentroCosto")]
        //public virtual NivelCentroCosto IdNivelCentroCostoNavigation { get; set; }


        //[InverseProperty("IdCentroCostoPadreNavigation")]
        //public virtual ICollection<CentroCosto> InverseIdCentroCostoPadreNavigation { get; set; } = new List<CentroCosto>();

    }
}