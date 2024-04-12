namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMSociosGrupos
    {
        public int IdSocio { get; set; }
        public int IdTipoSocio { get; set; }
        public int IdTipoDcmtoIdentidad { get; set; }
        public string NumeroDcmtoIdentidad { get; set; }      
        public string RazonSocial { get; set; }      
        public bool EsActivo { get; set; }

        public string CodigoTipoDcmto { get; set; }     
        public string NameTipoDcmto { get; set; }   
                                                       
        public List<VMTipoDocumentoIdentidad> TiposDcmtoIdentidad { get; set; }

    }
}