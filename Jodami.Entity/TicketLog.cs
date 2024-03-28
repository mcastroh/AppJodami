using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class TicketLog
{
    /// <summary>
    /// Ticket Log ID
    /// </summary>
    public int IdTicketLog { get; set; }

    /// <summary>
    /// Ticket ID
    /// </summary>
    public int IdTicket { get; set; }

    /// <summary>
    /// Motivo del Log
    /// </summary>
    public string MotivoCrud { get; set; }

    /// <summary>
    /// Decía
    /// </summary>
    public string DatoDecia { get; set; }

    /// <summary>
    /// Dice
    /// </summary>
    public string DatoDice { get; set; }

    /// <summary>
    /// Auditoría Usuario
    /// </summary>
    public string UsuarioName { get; set; }

    /// <summary>
    /// Auditoría Fecha
    /// </summary>
    public DateTime FechaRegistro { get; set; }

    public virtual Ticket IdTicketNavigation { get; set; }
}
