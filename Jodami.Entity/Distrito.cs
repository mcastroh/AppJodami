using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class Distrito
{
    /// <summary>
    /// Distrito ID
    /// </summary>
    public int IdDistrito { get; set; }

    /// <summary>
    /// Código
    /// </summary>
    public string CodigoDistrito { get; set; }

    /// <summary>
    /// Distrito
    /// </summary>
    public string Distrito1 { get; set; }

    /// <summary>
    /// Provincia ID
    /// </summary>
    public int IdProvincia { get; set; }

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

    public virtual Provincium IdProvinciaNavigation { get; set; }
}
