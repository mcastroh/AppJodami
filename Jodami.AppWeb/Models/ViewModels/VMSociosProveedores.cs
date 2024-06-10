namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMSociosProveedores : VMSociosGrupos
    {
        public string OperacionCRUD { get; set; }
        public int IdTipoDcmtoIdentidadAsignado { get; set; }

        public string NombreRazonSocial { get; set; }

        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }

        public int? IdGrupoSocioNegocio { get; set; }
        public int? IdColaboradorAsignado { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string PaginaWeb { get; set; }
        public string Email { get; set; }
        public bool IsAfectoRetencion { get; set; }
        public bool IsAfectoPercepcion { get; set; }
        public bool IsBuenContribuyente { get; set; }
        public int? IdTipoCalificacion { get; set; }
        public string ZonaPostal { get; set; }
        public DateTime? FechaInicioOperaciones { get; set; }
        public int? IdTipoMotivoBaja { get; set; }
        public DateTime? FechaBaja { get; set; }

        public int KeyIdTipoDcmtoIdentidadRUC { get; set; }

        public List<VMSociosGrupos> nav_GrupoEconomico { get; set; }
        public List<VMSociosColaboradores> nav_Colaboradores { get; set; }
        public List<VMTipoCalificacion> nav_Calificacion { get; set; }

         
    }
}