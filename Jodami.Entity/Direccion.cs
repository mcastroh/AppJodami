using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class Direccion
{
    /// <summary>
    /// Dirección ID
    /// </summary>
    public int IdDireccion { get; set; }

    /// <summary>
    /// Socio ID
    /// </summary>
    public int IdSocio { get; set; }

    /// <summary>
    /// Tipo Direccón ID
    /// </summary>
    public int IdTipoDireccion { get; set; }

    /// <summary>
    /// Tipo Via ID
    /// </summary>
    public int IdTipoVia { get; set; }

    /// <summary>
    /// Nombre Tipo Via
    /// </summary>
    public string NombreVia { get; set; }

    /// <summary>
    /// Número Tipo Via
    /// </summary>
    public string NumeroVia { get; set; }

    /// <summary>
    /// Tipo Zona ID
    /// </summary>
    public int IdTipoZona { get; set; }

    /// <summary>
    /// Nombre Tipo Zona
    /// </summary>
    public string NombreZona { get; set; }

    /// <summary>
    /// Distrito ID
    /// </summary>
    public int IdDistrito { get; set; }

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

    public virtual ICollection<EmpresaLocal> EmpresaLocals { get; set; } = new List<EmpresaLocal>();

    public virtual ICollection<Empresa> Empresas { get; set; } = new List<Empresa>();

    public virtual Distrito IdDistritoNavigation { get; set; }

    public virtual TipoDireccion IdTipoDireccionNavigation { get; set; }

    public virtual TipoVium IdTipoViaNavigation { get; set; }

    public virtual TipoZona IdTipoZonaNavigation { get; set; }

    public virtual SocioDireccion SocioDireccion { get; set; }
}
