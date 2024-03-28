namespace Jodami.Entity;

public partial class Moneda
{
    /// <summary>
    /// Moneda ID
    /// </summary>     
    /// [Key]
    public int IdMoneda { get; set; }

    /// <summary>
    /// Descripción
    /// </summary>
    public string Descripcion { get; set; }

    /// <summary>
    /// Símbolo
    /// </summary>
    public string Simbolo { get; set; }

    /// <summary>
    /// Código SUNAT
    /// </summary>
    public string IdSunat { get; set; }

    public int Orden { get; set; }

    /// <summary>
    /// ¿Es Activo?
    /// </summary>
    public bool EsActivo { get; set; }

    /// <summary>
    /// Auditoría Usuario
    /// </summary>
    public string UsuarioName { get; set; }

    /// <summary>
    /// Auditoría Fecha
    /// </summary>
    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<SocioCuentaBanco> SocioCuentaBancos { get; set; } = new List<SocioCuentaBanco>();

    public virtual ICollection<SocioPrecioArticulo> SocioPrecioArticulos { get; set; } = new List<SocioPrecioArticulo>();
}
