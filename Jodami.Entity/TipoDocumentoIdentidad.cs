using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class TipoDocumentoIdentidad
{
    /// <summary>
    /// Tipo Dcmto ID
    /// </summary>
    public int IdTipoDcmtoIdentidad { get; set; }

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
    public string IdCodigoSunat { get; set; }

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

    public virtual ICollection<Chofere> Choferes { get; set; } = new List<Chofere>();

    public virtual ICollection<Socio> Socios { get; set; } = new List<Socio>();
}
