﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Jodami.Entity;

public partial class TicketPesajes
{
    /// <summary>
    /// Ticket Pesaje ID
    /// </summary>
    [Key]
    public int IdTicketPesaje { get; set; }

    /// <summary>
    /// Ticket ID
    /// </summary>
    public int IdTicket { get; set; }

    /// <summary>
    /// Nro Pesaje
    /// </summary>
    public int NumeroPesaje { get; set; }

    /// <summary>
    /// Artículo ID
    /// </summary>
    public int? IdArticulo { get; set; }

    /// <summary>
    /// Peso Balanza
    /// </summary>
    [Column(TypeName = "decimal(21, 3)")]
    public decimal PesoBalanza { get; set; }

    /// <summary>
    /// Unidad Pesaje
    /// </summary>
    public int IdUnidad { get; set; }

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

    [ForeignKey("IdArticulo")]
    [InverseProperty("TicketPesajes")]
    public virtual Articulo IdArticuloNavigation { get; set; }

    [ForeignKey("IdTicket")]
    [InverseProperty("TicketPesajes")]
    public virtual Ticket IdTicketNavigation { get; set; }
}