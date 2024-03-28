using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class TipoZona
{
    /// <summary>
    /// Tipo Zona ID
    /// </summary>
    public int IdTipoZona { get; set; }

    /// <summary>
    /// Código TipoZona
    /// </summary>
    public string CodigoTipoZona { get; set; }

    /// <summary>
    /// Descripción
    /// </summary>
    public string Descripcion { get; set; }

    /// <summary>
    /// Orden Presentación
    /// </summary>
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

    public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();
}
