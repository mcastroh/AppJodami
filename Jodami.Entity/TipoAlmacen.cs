using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class TipoAlmacen
{
    /// <summary>
    /// Tipo Almacén ID
    /// </summary>
    public int IdTipoAlmacen { get; set; }

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
    public bool EsActivo { get; set; }

    /// <summary>
    /// Auditoría Usuario
    /// </summary>
    public string UsuarioName { get; set; }

    /// <summary>
    /// Auditoría Fecha
    /// </summary>
    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Almacen> Almacens { get; set; } = new List<Almacen>();
}
