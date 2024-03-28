using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class SocioFormaPago
{
    /// <summary>
    /// Forma Pago ID
    /// </summary>
    public int IdFormaPago { get; set; }

    /// <summary>
    /// Socio ID
    /// </summary>
    public int IdSocio { get; set; }

    /// <summary>
    /// Tipo Forma Pago ID
    /// </summary>
    public int IdTipoFormaPago { get; set; }

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

    public virtual Socio IdSocioNavigation { get; set; }

    public virtual TipoFormaPago IdTipoFormaPagoNavigation { get; set; }
}
