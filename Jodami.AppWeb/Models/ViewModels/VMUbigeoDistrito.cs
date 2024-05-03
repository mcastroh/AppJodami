namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMUbigeoDistrito
    {
        public int IdDistrito { get; set; } 
        public string CodigoDistrito { get; set; } 
        public string DistritoName { get; set; }
        
        public int UbiProvKey { get; set; }
        public string UbiProvCodigo { get; set; }
        public string UbiProvName { get; set; }

        public int UbiDptoKey { get; set; }
        public string UbiDptoCodigo { get; set; }
        public string UbiDptoName { get; set; } 

    }
}
    