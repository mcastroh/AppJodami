namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMSociosColaboradores : VMSociosGrupos
    {
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }             
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string PaginaWeb { get; set; }
        public string Email { get; set; }         

        public string ApellidosAndNombres { get; set; }
         
    }
}