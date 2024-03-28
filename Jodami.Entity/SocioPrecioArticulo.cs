using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class SocioPrecioArticulo
{
    /// <summary>
    /// Precio Artículo ID
    /// </summary>
    public int IdPrecioArticulo { get; set; }

    /// <summary>
    /// Socio ID
    /// </summary>
    public int IdSocio { get; set; }

    /// <summary>
    /// Artículo ID
    /// </summary>
    public int IdArticulo { get; set; }

    /// <summary>
    /// Moneda ID
    /// </summary>
    public int IdMoneda { get; set; }

    /// <summary>
    /// Precio Unitario
    /// </summary>
    public decimal PrecioUnitario { get; set; }

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

    public virtual Articulo IdArticuloNavigation { get; set; }

    public virtual Moneda IdMonedaNavigation { get; set; }

    public virtual Socio IdSocioNavigation { get; set; }
}
