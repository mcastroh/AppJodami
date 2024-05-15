namespace Jodami.AppWeb.Utilidades.Infraestructura
{
    public class KeysNames
    {
        // 
        // Tipos de Socios Comerciales
        public const string GRUPOS_ECONOMICOS = "GRUPOS ECONOMICOS";
        public const string PROVEEDORES = "PROVEEDORES";
        public const string CLIENTES = "CLIENTES";
        public const string CLIENTES_POTENCIALES = "CLIENTES POTENCIALES";
        public const string COMPRADORES = "COMPRADORES";
        public const string VENDEDORES = "VENDEDORES";
        public const string COLABORADORES = "COLABORADORES";

        // 
        // Tipos de Documento de Identidad
        public const string TIPO_DCMTO_IDENTIDAD_OTR = "OTR";
        public const string TIPO_DCMTO_IDENTIDAD_RUC = "RUC";
        public const string TIPO_DCMTO_IDENTIDAD_DNI = "DNI";

        //
        // Tipo Valorización 
        public const string TIPO_VALORIZACION_PROMEDIO = "PROMEDIO";
        public const string TIPO_VALORIZACION_ESTANDAR = "ESTANDAR";
        public const string TIPO_VALORIZACION_FIFO = "FIFO";
        public const string TIPO_VALORIZACION_LIFO = "LIFO";

        //
        // Contabilizar por 
        public const string CONTABILIZAR_POR_GRUPO_ARTICULO = "GRUPO ARTICULO";
        public const string CONTABILIZAR_POR_ALMACEN = "ALMACEN";
        public const string CONTABILIZAR_POR_ARTICULO = "ARTICULO";

        //
        // Tipos de Artículos 
        public const string TIPO_ARTICULO = "Artículo";
        public const string TIPO_SERVICIO = "Servicio";
        public const string TIPO_ACTIVO_FIJO = "Activo Fijo";


         

        public enum FormularioAccion
        {
            Nuevo,
            Actualizar,
            Eliminar,
            Consultar
        } 



    }
}