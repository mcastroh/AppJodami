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
        public string NumeroLote { get; set; }
        public int? IdUnidadGasto { get; set; }
        public bool EsActivo { get; set; } 

    }
}