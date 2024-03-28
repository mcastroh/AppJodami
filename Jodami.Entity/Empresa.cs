using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class Empresa
{
    /// <summary>
    /// Empresa ID
    /// </summary>
    public int IdEmpresa { get; set; }

    /// <summary>
    /// RUC
    /// </summary>
    public string NumeroRuc { get; set; }

    /// <summary>
    /// Razón Social
    /// </summary>
    public string RazonSocial { get; set; }

    /// <summary>
    /// Nombre Comercial
    /// </summary>
    public string NombreComercial { get; set; }

    /// <summary>
    /// Pagina Web
    /// </summary>
    public string PaginaWeb { get; set; }

    /// <summary>
    /// Dirección ID
    /// </summary>
    public int? IdDireccion { get; set; }

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

    /// <summary>
    /// Logo
    /// </summary>
    public byte[] Logo { get; set; }

    public virtual ICollection<EmpresaLocal> EmpresaLocals { get; set; } = new List<EmpresaLocal>();

    public virtual Direccion IdDireccionNavigation { get; set; }
}
