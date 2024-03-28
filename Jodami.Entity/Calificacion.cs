using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class Calificacion
{
    /// <summary>
    /// Calificación ID
    /// </summary>
    public int IdCalificacion { get; set; }

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
}
