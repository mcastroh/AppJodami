using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class Socio
{
    /// <summary>
    /// Socio ID
    /// </summary>
    public int IdSocio { get; set; }

    /// <summary>
    /// Tipo Socio ID
    /// </summary>
    public int IdTipoSocio { get; set; }

    /// <summary>
    /// Tipo Dcmto Identidad ID
    /// </summary>
    public int IdTipoDcmtoIdentidad { get; set; }

    /// <summary>
    /// Número Dcmto Identidad
    /// </summary>
    public string NumeroDcmtoIdentidad { get; set; }

    /// <summary>
    /// Apellido Paterno
    /// </summary>
    public string ApellidoPaterno { get; set; }

    /// <summary>
    /// Apellido Materno
    /// </summary>
    public string ApellidoMaterno { get; set; }

    /// <summary>
    /// Primer Nombre
    /// </summary>
    public string PrimerNombre { get; set; }

    /// <summary>
    /// Segundo Nombre
    /// </summary>
    public string SegundoNombre { get; set; }

    /// <summary>
    /// Razón Social
    /// </summary>
    public string RazonSocial { get; set; }

    /// <summary>
    /// Grupo Económico ID
    /// </summary>
    public int? IdGrupoSocioNegocio { get; set; }

    /// <summary>
    /// Teléfono
    /// </summary>
    public string Telefono { get; set; }

    /// <summary>
    /// Celular
    /// </summary>
    public string Celular { get; set; }

    /// <summary>
    /// Página Web
    /// </summary>
    public string PaginaWeb { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Límite Crédito
    /// </summary>
    public decimal LimiteCredito { get; set; }

    /// <summary>
    /// Sobregiros
    /// </summary>
    public decimal Sobregiro { get; set; }

    /// <summary>
    /// ¿Afecto a Retención?
    /// </summary>
    public bool IsAfectoRetencion { get; set; }

    /// <summary>
    /// ¿Afecto a Percepción?
    /// </summary>
    public bool IsAfectoPercepcion { get; set; }

    /// <summary>
    /// ¿Buen Contribuyente?
    /// </summary>
    public bool IsBuenContribuyente { get; set; }

    /// <summary>
    /// Tipo Calificación ID
    /// </summary>
    public int? IdTipoCalificacion { get; set; }

    /// <summary>
    /// Zona Postal
    /// </summary>
    public string ZonaPostal { get; set; }

    /// <summary>
    /// Fecha Inicio Operaciones
    /// </summary>
    public DateTime? FechaInicioOperaciones { get; set; }

    /// <summary>
    /// Tipo Motivo Baja ID
    /// </summary>
    public int? IdTipoMotivoBaja { get; set; }

    /// <summary>
    /// Fecha Baja
    /// </summary>
    public DateTime? FechaBaja { get; set; }

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

    public virtual ICollection<Almacen> Almacens { get; set; } = new List<Almacen>();

    public virtual Socio IdGrupoSocioNegocioNavigation { get; set; }

    public virtual TipoCalificacion IdTipoCalificacionNavigation { get; set; }

    public virtual TipoDocumentoIdentidad IdTipoDcmtoIdentidadNavigation { get; set; }

    public virtual TipoMotivoBaja IdTipoMotivoBajaNavigation { get; set; }

    public virtual TipoSocio IdTipoSocioNavigation { get; set; }

    public virtual ICollection<Socio> InverseIdGrupoSocioNegocioNavigation { get; set; } = new List<Socio>();

    public virtual ICollection<SocioCuentaBanco> SocioCuentaBancos { get; set; } = new List<SocioCuentaBanco>();

    public virtual ICollection<SocioDireccion> SocioDireccions { get; set; } = new List<SocioDireccion>();

    public virtual ICollection<SocioFormaPago> SocioFormaPagos { get; set; } = new List<SocioFormaPago>();

    public virtual ICollection<SocioPrecioArticulo> SocioPrecioArticulos { get; set; } = new List<SocioPrecioArticulo>();
}
