namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMTipoDocumentoIdentidad
    {
        public int IdTipoDcmtoIdentidad { get; set; }             
        public string Descripcion { get; set; }
        public string Simbolo { get; set; }
        public string IdCodigoSunat { get; set; }
        public bool EsActivo { get; set; }

        public string SimboloAndDescripcion { get; set; }
    }
}
 