namespace Jodami.AppWeb.Models.ViewModels
{
    public class VMArticulos
    {
        public int IdArticulo { get; set; }
        public string CodigoArticulo { get; set; }
        public string Descripcion { get; set; }
        public int IdSubGrupoArticulo { get; set; }
        public int? IdUnidadInventario { get; set; }
        public int? IdUnidadCompra { get; set; }
        public int? IdUnidadVenta { get; set; }
        public int? IdTipoArticulo { get; set; }
        public int? IdTipoDetraccion { get; set; }
        public int? IdTipoValorizacion { get; set; }
        public int? IdTipoExistencia { get; set; }
        public decimal StockMinimo { get; set; }
        public decimal StockMaximo { get; set; }
        public decimal StockSeguridad { get; set; }
        public string Observaciones { get; set; }
        public bool EsActivo { get; set; }

        public bool EsArticuloInventarios { get; set; }
        public bool EsArticuloCompras { get; set; }
        public bool EsArticuloVentas { get; set; }

        public int? AlmacenDefaultId { get; set; }
        public int? IdTipoCuentaMayor { get; set; }
        public bool IsAplicarImpuestos { get; set; }
        public bool IsAfectoDetracciones { get; set; }
        public string CodigoOSCE { get; set; }
        public string NumeroParteFabricante { get; set; }

        public int IdGrupoArticuloKey { get; set; }

        public VMTipoArticulo Nav_TipoArticulo { get; set; }
        public VMGrupoArticulo Nav_GrupoArticulo { get; set; }
        public VMSubGrupoArticulo Nav_SubGrupoArticulo { get; set; }
        public VMUnidadMedida Nav_UnidadMedidaInventario { get; set; }



        public List<VMTipoArticulo> List_TipoArticulo { get; set; }
        public List<VMGrupoArticulo> List_GrupoArticulo { get; set; }
        public List<VMSubGrupoArticulo> List_SubGrupoArticulo { get; set; }
        public List<VMUnidadMedida> List_UnidadMedida { get; set; }
        public List<VMSunatTipoDetraccion> List_SunatTipoDetraccion { get; set; }
        public List<VMSunatTipoExistencia> List_SunatTipoExistencia { get; set; }
        public List<VMAlmacen> List_Almacen { get; set; }
        public List<VMTipoCuentaMayor> List_TipoCuentaMayor { get; set; }
        public List<VMTipoValorizacion> List_TipoValorizacion { get; set; }

    }
}