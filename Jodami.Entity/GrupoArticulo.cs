using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class GrupoArticulo
{
    /// <summary>
    /// Grupo ID
    /// </summary>
    public int IdGrupoArticulo { get; set; }

    /// <summary>
    /// Código
    /// </summary>
    public string Codigo { get; set; }

    /// <summary>
    /// Descripción
    /// </summary>
    public string Descripcion { get; set; }

    /// <summary>
    /// ¿Es Activo?
    /// </summary>
    public bool Activo { get; set; }

    /// <summary>
    /// Auditoría Usuario
    /// </summary>
    public string UsuarioName { get; set; }

    /// <summary>
    /// Auditoría Fecha
    /// </summary>
    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<SubGrupoArticulo> SubGrupoArticulos { get; set; } = new List<SubGrupoArticulo>();
}
