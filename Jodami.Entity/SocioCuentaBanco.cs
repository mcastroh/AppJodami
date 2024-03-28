using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class SocioCuentaBanco
{
    /// <summary>
    /// Cuenta Bancaria ID
    /// </summary>
    public int IdCuenta { get; set; }

    /// <summary>
    /// Socio ID
    /// </summary>
    public int IdSocio { get; set; }

    /// <summary>
    /// Entidad Financiera ID
    /// </summary>
    public int IdEntidadFinanciera { get; set; }

    /// <summary>
    /// Moneda ID
    /// </summary>
    public int IdMoneda { get; set; }

    /// <summary>
    /// Tipo Cta Bancaria ID
    /// </summary>
    public int IdTipoCuentaBancaria { get; set; }

    /// <summary>
    /// Código Cuenta
    /// </summary>
    public string CodigoCuenta { get; set; }

    /// <summary>
    /// Código Cuenta Interbancaria
    /// </summary>
    public string CodigoCuentaInterbancaria { get; set; }

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

    public virtual EntidadFinanciera IdEntidadFinancieraNavigation { get; set; }

    public virtual Moneda IdMonedaNavigation { get; set; }

    public virtual Socio IdSocioNavigation { get; set; }

    public virtual TipoCuentaBancarium IdTipoCuentaBancariaNavigation { get; set; }
}
