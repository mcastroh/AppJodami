using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class Almacen
{
    /// <summary>
    /// Almacén ID
    /// </summary>
    public int IdAlmacen { get; set; }

    /// <summary>
    /// Código
    /// </summary>
    public string Codigo { get; set; }

    /// <summary>
    /// Descripción
    /// </summary>
    public string Descripcion { get; set; }

    /// <summary>
    /// Local ID
    /// </summary>
    public int? IdLocal { get; set; }

    /// <summary>
    /// Tipo ID
    /// </summary>
    public int IdTipoAlmacen { get; set; }

    /// <summary>
    /// Dirección ID
    /// </summary>
    public int? IdDireccion { get; set; }

    /// <summary>
    /// Responsable Id
    /// </summary>
    public int? IdResponsable { get; set; }

    /// <summary>
    /// Area Metros Cuadrados
    /// </summary>
    public decimal Superficie { get; set; }

    /// <summary>
    /// Capacidad Kg
    /// </summary>
    public decimal Capacidad { get; set; }

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

    public virtual Direccion IdDireccionNavigation { get; set; }

    public virtual EmpresaLocal IdLocalNavigation { get; set; }

    public virtual Socio IdResponsableNavigation { get; set; }

    public virtual TipoAlmacen IdTipoAlmacenNavigation { get; set; }

    public virtual ICollection<Ticket> TicketIdAlmacenDestinoNavigations { get; set; } = new List<Ticket>();

    public virtual ICollection<Ticket> TicketIdAlmacenOrigenNavigations { get; set; } = new List<Ticket>();
}
