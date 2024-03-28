using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class RolMenu
{
    /// <summary>
    /// Rol Menú ID
    /// </summary>
    public int IdRolMenu { get; set; }

    /// <summary>
    /// Rol ID
    /// </summary>
    public int? IdRol { get; set; }

    /// <summary>
    /// Menú ID
    /// </summary>
    public int? IdMenu { get; set; }

    /// <summary>
    /// ¿Es Activo?
    /// </summary>
    public bool? EsActivo { get; set; }

    /// <summary>
    /// Auditoría Usuario
    /// </summary>
    public string UsuarioName { get; set; }

    /// <summary>
    /// Auditoría Fecha
    /// </summary>
    public DateTime? FechaRegistro { get; set; }

    public virtual Menu IdMenuNavigation { get; set; }

    public virtual Rol IdRolNavigation { get; set; }
}
