using System.ComponentModel.DataAnnotations;

namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMTipoVia
    {
        public int IdTipoVia { get; set; } 
        public string CodigoTipoVia { get; set; }         
        public string Descripcion { get; set; } 
        public bool EsActivo { get; set; }
         
    }
}
    