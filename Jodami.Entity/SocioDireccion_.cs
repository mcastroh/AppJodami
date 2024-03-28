﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class SocioDireccion_
{
    /// <summary>
    /// Dirección ID
    /// </summary>
    public int IdDireccion { get; set; }

    /// <summary>
    /// Socio ID
    /// </summary>
    public int SocioId { get; set; }

    /// <summary>
    /// Dirección Asociada
    /// </summary>
    public int DireccionId { get; set; }

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

    public virtual Direccion_ IdDireccionNavigation { get; set; }

    public virtual Socio Socio { get; set; }
}