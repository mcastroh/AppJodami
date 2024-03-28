using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class Vehiculo
{
    /// <summary>
    /// Vehículo ID
    /// </summary>
    public int IdVehiculo { get; set; }

    /// <summary>
    /// Nombre
    /// </summary>
    public string Nombre { get; set; }

    /// <summary>
    /// Marca
    /// </summary>
    public string Marca { get; set; }

    /// <summary>
    /// Modelo
    /// </summary>
    public string Modelo { get; set; }

    /// <summary>
    /// Color
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    /// Placa
    /// </summary>
    public string Placa { get; set; }

    /// <summary>
    /// Certificado Inscripción
    /// </summary>
    public string Certificado { get; set; }

    /// <summary>
    /// Peso en Kg
    /// </summary>
    public decimal PesoKg { get; set; }

    /// <summary>
    /// ¿Es de la Empresa?
    /// </summary>
    public bool EsDeEmpresa { get; set; }

    /// <summary>
    /// Flete ID
    /// </summary>
    public int? IdFlete { get; set; }

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

    public virtual TipoFlete IdFleteNavigation { get; set; }
}
