namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMSubGrupoArticulo
    {
        public int IdSubGrupoArticulo { get; set; } 
        public int IdGrupoArticulo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}