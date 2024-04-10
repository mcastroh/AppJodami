namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMSocios
    {
        public int IdSocio { get; set; }
        public int IdTipoSocio { get; set; }
        public int IdTipoDcmtoIdentidad { get; set; }
        public string NumeroDcmtoIdentidad { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string RazonSocial { get; set; }
        public int? IdGrupoSocioNegocio { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string PaginaWeb { get; set; }
        public string Email { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal Sobregiro { get; set; }
        public bool IsAfectoRetencion { get; set; }
        public bool IsAfectoPercepcion { get; set; }
        public bool IsBuenContribuyente { get; set; }
        public int? IdTipoCalificacion { get; set; }
        public string ZonaPostal { get; set; }
        public DateTime? FechaInicioOperaciones { get; set; }
        public int? IdTipoMotivoBaja { get; set; }      
        public DateTime? FechaBaja { get; set; }
        public bool EsActivo { get; set; }

        public List<VMTipoDocumentoIdentidad> TiposDcmtoIdentidad { get; set; }
        
    }
}