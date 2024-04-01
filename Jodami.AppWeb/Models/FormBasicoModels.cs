using System.ComponentModel;

namespace Jodami.AppWeb.Models
{
    public class FormBasicoModels
    {
        [DisplayName("Documento")]
        public string doc { get; set; }

        [DisplayName("Nombre")]
        public string nombre { get; set; }
    }
}