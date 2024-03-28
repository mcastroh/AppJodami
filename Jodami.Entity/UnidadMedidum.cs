using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class UnidadMedidum
{
    /// <summary>
    /// Unidad ID
    /// </summary>
    public int IdUnidad { get; set; }

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

    public virtual ICollection<Articulo> ArticuloIdUnidadCompraNavigations { get; set; } = new List<Articulo>();

    public virtual ICollection<Articulo> ArticuloIdUnidadInventarioNavigations { get; set; } = new List<Articulo>();

    public virtual ICollection<Articulo> ArticuloIdUnidadVentaNavigations { get; set; } = new List<Articulo>();
}
