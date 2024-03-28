using System;
using System.Collections.Generic;

namespace Jodami.Entity;

public partial class Usuario
{
    /// <summary>
    /// Usuario ID
    /// </summary>
    public int IdUsuario { get; set; }

    /// <summary>
    /// Nombre
    /// </summary>
    public string Nombre { get; set; }

    /// <summary>
    /// Correo
    /// </summary>
    public string Correo { get; set; }

    /// <summary>
    /// Celular
    /// </summary>
    public string Telefono { get; set; }

    /// <summary>
    /// Rol ID
    /// </summary>
    public int? IdRol { get; set; }

    /// <summary>
    /// Url Foto
    /// </summary>
    public string UrlFoto { get; set; }

    /// <summary>
    /// Nombre Foto
    /// </summary>
    public string NombreFoto { get; set; }

    /// <summary>
    /// Clave
    /// </summary>
    public string Clave { get; set; }

    /// <summary>
    /// ¿Es Activo?
    /// </summary>
    public bool? EsActivo { get; set; }

    /// <summary>
    /// Auditoría Usuario
    /// </summary>
    public string UsuarioName { get; set; }

    /// <summary>
    /// Auditoría Fecha
    /// </summary>
    public DateTime? FechaRegistro { get; set; }

    public virtual Rol IdRolNavigation { get; set; }
}
