﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Jodami.Entity;

public partial class Empresa
{
    /// <summary>
    /// Empresa Id
    /// </summary>
    [Key]
    public int IdEmpresa { get; set; }

    /// <summary>
    /// Número RUC
    /// </summary>
    [Required]
    [StringLength(20)]
    public string NumeroRUC { get; set; }

    /// <summary>
    /// Razón Social
    /// </summary>
    [Required]
    [StringLength(100)]
    public string RazonSocial { get; set; }

    /// <summary>
    /// Nombre Comercial
    /// </summary>
    [Required]
    [StringLength(100)]
    public string NombreComercial { get; set; }

    /// <summary>
    /// Página Web
    /// </summary>
    [Required]
    [StringLength(100)]
    public string PaginaWeb { get; set; }

    /// <summary>
    /// Dirección ID
    /// </summary>
    public int? IdDireccion { get; set; }

    /// <summary>
    /// Logo
    /// </summary>
    public int? IdImagen { get; set; }

    /// <summary>
    /// ¿Es Activo?
    /// </summary>
    public bool EsActivo { get; set; }

    /// <summary>
    /// Auditoría Usuario
    /// </summary>
    [Required]
    [StringLength(60)]
    public string UsuarioName { get; set; }

    /// <summary>
    /// Auditoría Fecha
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [InverseProperty("IdEmpresaNavigation")]
    public virtual ICollection<EmpresaLocal> EmpresaLocal { get; set; } = new List<EmpresaLocal>();

    [ForeignKey("IdDireccion")]
    [InverseProperty("Empresa")]
    public virtual Direccion IdDireccionNavigation { get; set; }

    [ForeignKey("IdImagen")]
    [InverseProperty("Empresa")]
    public virtual Imagenes IdImagenNavigation { get; set; }
}