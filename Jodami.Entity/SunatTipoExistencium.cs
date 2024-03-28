using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class SunatTipoExistencium
{
    /// <summary>
    /// Tipo Existencia ID
    /// </summary>
    public int IdTipoExistencia { get; set; }

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
    /// Auditoría Fecha
    /// </summary>
    public string UsuarioName { get; set; }

    /// <summary>
    /// Auditoría Fecha
    /// </summary>
    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();
}
