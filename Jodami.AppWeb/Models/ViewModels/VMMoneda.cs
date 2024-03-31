using System.ComponentModel.DataAnnotations;

namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMMoneda
    {
        [Key]
        [Display(Name = "Moneda ID")]
        public int IdMoneda { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Descripcion es obligatorio")]
        [MaxLength(60)]
        public string Descripcion { get; set; }

        [Display(Name = "Símbolo")]
        [Required(ErrorMessage = "Símbolo de moneda es obligatorio")]
        [MaxLength(20)]
        public string Simbolo { get; set; }

        [Display(Name = "Código SUNAT")]
        [Required(ErrorMessage = "Código SUNAT es obligatorio")]
        [MaxLength(20)]
        public string IdSunat { get; set; }

        [Display(Name = "Orden de presentación")]
        public int Orden { get; set; }

        [Display(Name = "Estado")]
        public bool EsActivo { get; set; }    
        
    }
}
    