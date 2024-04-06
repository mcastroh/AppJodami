using System.ComponentModel.DataAnnotations;

namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMTipoZona
    {
        public int IdTipoZona { get; set; } 
        public string CodigoTipoZona { get; set; }         
        public string Descripcion { get; set; } 
        public bool EsActivo { get; set; }
         
    }
}
    