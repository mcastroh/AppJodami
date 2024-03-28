using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class Departamento
{
    /// <summary>
    /// Departamento ID
    /// </summary>
    public int IdDepartamento { get; set; }

    /// <summary>
    /// Código
    /// </summary>
    public string CodigoDepartamento { get; set; }

    /// <summary>
    /// Departamento
    /// </summary>
    public string Departamento1 { get; set; }

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

    public virtual ICollection<Provincium> Provincia { get; set; } = new List<Provincium>();
}
