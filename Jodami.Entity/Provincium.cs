using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class Provincium
{
    /// <summary>
    /// Provincia ID
    /// </summary>
    public int IdProvincia { get; set; }

    /// <summary>
    /// Código
    /// </summary>
    public string CodigoProvincia { get; set; }

    /// <summary>
    /// Provincia
    /// </summary>
    public string Provincia { get; set; }

    /// <summary>
    /// Departamento ID
    /// </summary>
    public int IdDepartamento { get; set; }

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

    public virtual ICollection<Distrito> Distritos { get; set; } = new List<Distrito>();

    public virtual Departamento IdDepartamentoNavigation { get; set; }
}
