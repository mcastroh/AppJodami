using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class Ticket
{
    /// <summary>
    /// Ticket ID
    /// </summary>
    public int IdTicket { get; set; }

    /// <summary>
    /// NúmeroTicket
    /// </summary>
    public int NumeroTicket { get; set; }

    /// <summary>
    /// Fecha Pesaje
    /// </summary>
    public DateTime FechaPesaje { get; set; }

    /// <summary>
    /// Chofer ID
    /// </summary>
    public int IdChofer { get; set; }

    /// <summary>
    /// Vehículo ID
    /// </summary>
    public int IdVehiculo { get; set; }

    /// <summary>
    /// Transportista ID
    /// </summary>
    public int? IdTransportista { get; set; }

    /// <summary>
    /// Tipo Movto ID
    /// </summary>
    public int? IdTipoMovimiento { get; set; }

    /// <summary>
    /// Almacén Origen ID
    /// </summary>
    public int? IdAlmacenOrigen { get; set; }

    /// <summary>
    /// Almacén Destino ID
    /// </summary>
    public int? IdAlmacenDestino { get; set; }

    /// <summary>
    /// Serie Guía Remisión
    /// </summary>
    public string GuiaRemisionSerie { get; set; }

    /// <summary>
    /// Nro Guía Remisión
    /// </summary>
    public string GuiaRemisionNumero { get; set; }

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

    public virtual Almacen IdAlmacenDestinoNavigation { get; set; }

    public virtual Almacen IdAlmacenOrigenNavigation { get; set; }

    public virtual TipoMovimiento IdTipoMovimientoNavigation { get; set; }

    public virtual ICollection<TicketLog> TicketLogs { get; set; } = new List<TicketLog>();

    public virtual ICollection<TicketPesaje> TicketPesajes { get; set; } = new List<TicketPesaje>();
}
