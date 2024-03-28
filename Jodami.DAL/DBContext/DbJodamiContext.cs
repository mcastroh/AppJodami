using Jodami.Entity;
using Microsoft.EntityFrameworkCore;

namespace Jodami.DAL.DBContext;


public partial class DbJodamiContext : DbContext
{
    public DbJodamiContext(DbContextOptions<DbJodamiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Almacen> Almacen { get; set; }

    public virtual DbSet<Articulo> Articulo { get; set; }

    public virtual DbSet<ArticuloImagen> ArticuloImagen { get; set; }

    public virtual DbSet<Calificacion> Calificacion { get; set; }

    public virtual DbSet<Choferes> Choferes { get; set; }

    public virtual DbSet<Departamento> Departamento { get; set; }

    public virtual DbSet<Direccion_> Direccion_ { get; set; }

    public virtual DbSet<Distrito> Distrito { get; set; }

    public virtual DbSet<Empresa> Empresa { get; set; }

    public virtual DbSet<EmpresaLocal> EmpresaLocal { get; set; }

    public virtual DbSet<EntidadFinanciera> EntidadFinanciera { get; set; }

    public virtual DbSet<GrupoArticulo> GrupoArticulo { get; set; }

    public virtual DbSet<Menu> Menu { get; set; }

    public virtual DbSet<Moneda> Moneda { get; set; }

    public virtual DbSet<Proceso> Proceso { get; set; }

    public virtual DbSet<Provincia> Provincia { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<RolMenu> RolMenu { get; set; }

    public virtual DbSet<Socio> Socio { get; set; }

    public virtual DbSet<SocioCuentaBanco> SocioCuentaBanco { get; set; }

    public virtual DbSet<SocioDireccion_> SocioDireccion_ { get; set; }

    public virtual DbSet<SocioFormaPago> SocioFormaPago { get; set; }

    public virtual DbSet<SocioPrecioArticulo> SocioPrecioArticulo { get; set; }

    public virtual DbSet<SubGrupoArticulo> SubGrupoArticulo { get; set; }

    public virtual DbSet<SubTipoMovimiento> SubTipoMovimiento { get; set; }

    public virtual DbSet<SunatTipoDetraccion> SunatTipoDetraccion { get; set; }

    public virtual DbSet<SunatTipoExistencia> SunatTipoExistencia { get; set; }

    public virtual DbSet<Ticket> Ticket { get; set; }

    public virtual DbSet<TicketLog> TicketLog { get; set; }

    public virtual DbSet<TicketPesajes> TicketPesajes { get; set; }

    public virtual DbSet<TipoAlmacen> TipoAlmacen { get; set; }

    public virtual DbSet<TipoArticulo> TipoArticulo { get; set; }

    public virtual DbSet<TipoCalificacion> TipoCalificacion { get; set; }

    public virtual DbSet<TipoCuentaBancaria> TipoCuentaBancaria { get; set; }

    public virtual DbSet<TipoDireccion> TipoDireccion { get; set; }

    public virtual DbSet<TipoDocumentoIdentidad> TipoDocumentoIdentidad { get; set; }

    public virtual DbSet<TipoFlete> TipoFlete { get; set; }

    public virtual DbSet<TipoFormaPago> TipoFormaPago { get; set; }

    public virtual DbSet<TipoMotivoBaja> TipoMotivoBaja { get; set; }

    public virtual DbSet<TipoMovimiento> TipoMovimiento { get; set; }

    public virtual DbSet<TipoSocio> TipoSocio { get; set; }

    public virtual DbSet<TipoValorizacion> TipoValorizacion { get; set; }

    public virtual DbSet<TipoVia> TipoVia { get; set; }

    public virtual DbSet<TipoZona> TipoZona { get; set; }

    public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<Vehiculos> Vehiculos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Almacen>(entity =>
        {
            entity.HasKey(e => e.IdAlmacen).HasName("PK__Almacen__7339837BC61BEFD9");

            entity.Property(e => e.IdAlmacen).HasComment("Almacén ID");
            entity.Property(e => e.Capacidad)
                .HasComment("Capacidad Kg")
                .HasColumnType("decimal(19, 6)");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("Código");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdDireccion).HasComment("Dirección ID");
            entity.Property(e => e.IdLocal).HasComment("Local ID");
            entity.Property(e => e.IdResponsable).HasComment("Responsable Id");
            entity.Property(e => e.IdTipoAlmacen).HasComment("Tipo ID");
            entity.Property(e => e.Superficie)
                .HasComment("Area Metros Cuadrados")
                .HasColumnType("decimal(19, 6)");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdDireccionNavigation).WithMany(p => p.Almacen)
                .HasForeignKey(d => d.IdDireccion)
                .HasConstraintName("FK_Almacen_Direccion ");

            entity.HasOne(d => d.IdLocalNavigation).WithMany(p => p.Almacen)
                .HasForeignKey(d => d.IdLocal)
                .HasConstraintName("FK_Almacen_EmpresaLocal");

            entity.HasOne(d => d.IdResponsableNavigation).WithMany(p => p.Almacen)
                .HasForeignKey(d => d.IdResponsable)
                .HasConstraintName("FK_Almacen_Socio");

            entity.HasOne(d => d.IdTipoAlmacenNavigation).WithMany(p => p.Almacen)
                .HasForeignKey(d => d.IdTipoAlmacen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Almacen_TipoAlmacen");
        });

        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.IdArticulo).HasName("PK__Articulo__F8FF5D5290FA4397");

            entity.Property(e => e.IdArticulo).HasComment("Artículo ID");
            entity.Property(e => e.CodigoArticulo)
                .IsRequired()
                .HasMaxLength(10)
                .HasComment("Código Artículo");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdSubGrupoArticulo).HasComment("Sub Grupo Artículo ID");
            entity.Property(e => e.IdTipoArticulo).HasComment("Tipo Artículo ID");
            entity.Property(e => e.IdTipoDetraccion).HasComment("Tipo Detracción Sunat ID");
            entity.Property(e => e.IdTipoExistencia).HasComment("Tipo Existencia Sunat ID");
            entity.Property(e => e.IdTipoValorizacion).HasComment("Tipo Valorización ID");
            entity.Property(e => e.IdUnidadCompra).HasComment("Unidad Compra ID");
            entity.Property(e => e.IdUnidadInventario).HasComment("Unidad Inventario ID");
            entity.Property(e => e.IdUnidadVenta).HasComment("Unidad Venta ID");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(1000)
                .HasComment("Observaciones");
            entity.Property(e => e.StockMaximo)
                .HasComment("Stock Máximo")
                .HasColumnType("decimal(19, 6)");
            entity.Property(e => e.StockMinimo)
                .HasComment("Stock Mínimo")
                .HasColumnType("decimal(19, 6)");
            entity.Property(e => e.StockSeguridad)
                .HasComment("Stock de Seguridad")
                .HasColumnType("decimal(19, 6)");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdSubGrupoArticuloNavigation).WithMany(p => p.Articulo)
                .HasForeignKey(d => d.IdSubGrupoArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Articulo_SubGrupoArticulo");

            entity.HasOne(d => d.IdTipoArticuloNavigation).WithMany(p => p.Articulo)
                .HasForeignKey(d => d.IdTipoArticulo)
                .HasConstraintName("FK_Articulo_TipoArticulo");

            entity.HasOne(d => d.IdTipoDetraccionNavigation).WithMany(p => p.Articulo)
                .HasForeignKey(d => d.IdTipoDetraccion)
                .HasConstraintName("FK_Articulo_SunatTipoDetraccion");

            entity.HasOne(d => d.IdTipoExistenciaNavigation).WithMany(p => p.Articulo)
                .HasForeignKey(d => d.IdTipoExistencia)
                .HasConstraintName("FK_Articulo_SunatTipoExistencia");

            entity.HasOne(d => d.IdTipoValorizacionNavigation).WithMany(p => p.Articulo)
                .HasForeignKey(d => d.IdTipoValorizacion)
                .HasConstraintName("FK_Articulo_TipoValorizacion");

            entity.HasOne(d => d.IdUnidadCompraNavigation).WithMany(p => p.ArticuloIdUnidadCompraNavigation)
                .HasForeignKey(d => d.IdUnidadCompra)
                .HasConstraintName("FK_Articulo_UnidadMedida1");

            entity.HasOne(d => d.IdUnidadInventarioNavigation).WithMany(p => p.ArticuloIdUnidadInventarioNavigation)
                .HasForeignKey(d => d.IdUnidadInventario)
                .HasConstraintName("FK_Articulo_UnidadMedida");

            entity.HasOne(d => d.IdUnidadVentaNavigation).WithMany(p => p.ArticuloIdUnidadVentaNavigation)
                .HasForeignKey(d => d.IdUnidadVenta)
                .HasConstraintName("FK_Articulo_UnidadMedida2");
        });

        modelBuilder.Entity<ArticuloImagen>(entity =>
        {
            entity.HasKey(e => e.IdArticuloImagen).HasName("PK__Articulo__714752565DEFFA98");

            entity.Property(e => e.IdArticuloImagen).HasComment("Artículo Imagen ID");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdArticulo).HasComment("Artículo ID");
            entity.Property(e => e.Imagen)
                .IsRequired()
                .HasComment("Imagen");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.ArticuloImagen)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticuloImagen_Articulo");
        });

        modelBuilder.Entity<Calificacion>(entity =>
        {
            entity.HasKey(e => e.IdCalificacion).HasName("PK__Califica__40E4A75103B64D76");

            entity.Property(e => e.IdCalificacion).HasComment("Calificación ID");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.Orden).HasComment("Orden Presentación");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<Choferes>(entity =>
        {
            entity.HasKey(e => e.IdChofer).HasName("PK__Choferes__2DF292ADC7B83B95");

            entity.Property(e => e.IdChofer).HasComment("Chofer ID");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdTipoDcmto).HasComment("Tipo Dcmto Identidad ID");
            entity.Property(e => e.Licencia)
                .IsRequired()
                .HasMaxLength(40)
                .HasDefaultValue("")
                .HasComment("Licencia");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Nombres");
            entity.Property(e => e.NumeroDcmto)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("Nro Dcmto Identidad");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasDefaultValue("Admin")
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdTipoDcmtoNavigation).WithMany(p => p.Choferes)
                .HasForeignKey(d => d.IdTipoDcmto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Choferes_TipoDocumentoIdentidad");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__787A433DBEF3F418");

            entity.Property(e => e.IdDepartamento).HasComment("Departamento ID");
            entity.Property(e => e.CodigoDepartamento)
                .IsRequired()
                .HasMaxLength(4)
                .HasComment("Código");
            entity.Property(e => e.Departamento1)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Departamento")
                .HasColumnName("Departamento");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<Direccion_>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PK__Direccio__1F8E0C76DE9E0559");

            entity.ToTable("Direccion ");

            entity.Property(e => e.IdDireccion).HasComment("Dirección ID");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdDistrito).HasComment("Distrito ID");
            entity.Property(e => e.IdSocio).HasComment("Socio ID");
            entity.Property(e => e.IdTipoDireccion).HasComment("Tipo Direccón ID");
            entity.Property(e => e.IdTipoVia).HasComment("Tipo Via ID");
            entity.Property(e => e.IdTipoZona).HasComment("Tipo Zona ID");
            entity.Property(e => e.NombreVia)
                .HasMaxLength(100)
                .HasComment("Nombre Tipo Via");
            entity.Property(e => e.NombreZona)
                .HasMaxLength(100)
                .HasComment("Nombre Tipo Zona");
            entity.Property(e => e.NumeroVia)
                .HasMaxLength(100)
                .HasComment("Número Tipo Via");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdDistritoNavigation).WithMany(p => p.Direccion_)
                .HasForeignKey(d => d.IdDistrito)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Direccion _Distrito");

            entity.HasOne(d => d.IdTipoDireccionNavigation).WithMany(p => p.Direccion_)
                .HasForeignKey(d => d.IdTipoDireccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Direccion _TipoDireccion");

            entity.HasOne(d => d.IdTipoViaNavigation).WithMany(p => p.Direccion_)
                .HasForeignKey(d => d.IdTipoVia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Direccion _TipoVia");

            entity.HasOne(d => d.IdTipoZonaNavigation).WithMany(p => p.Direccion_)
                .HasForeignKey(d => d.IdTipoZona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Direccion _TipoZona");
        });

        modelBuilder.Entity<Distrito>(entity =>
        {
            entity.HasKey(e => e.IdDistrito).HasName("PK__Distrito__DE8EED5980F16A16");

            entity.Property(e => e.IdDistrito).HasComment("Distrito ID");
            entity.Property(e => e.CodigoDistrito)
                .IsRequired()
                .HasMaxLength(6)
                .HasComment("Código");
            entity.Property(e => e.Distrito1)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Distrito")
                .HasColumnName("Distrito");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdProvincia).HasComment("Provincia ID");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Distrito)
                .HasForeignKey(d => d.IdProvincia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Distrito_Provincia");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK__Empresa__5EF4033ED25EAF7B");

            entity.Property(e => e.IdEmpresa).HasComment("Empresa ID");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdDireccion).HasComment("Dirección ID");
            entity.Property(e => e.Logo).HasComment("Logo");
            entity.Property(e => e.NombreComercial)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Nombre Comercial");
            entity.Property(e => e.NumeroRUC)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("RUC");
            entity.Property(e => e.PaginaWeb)
                .HasMaxLength(100)
                .HasComment("Pagina Web");
            entity.Property(e => e.RazonSocial)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Razón Social");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdDireccionNavigation).WithMany(p => p.Empresa)
                .HasForeignKey(d => d.IdDireccion)
                .HasConstraintName("FK_Empresa_Direccion ");
        });

        modelBuilder.Entity<EmpresaLocal>(entity =>
        {
            entity.HasKey(e => e.IdLocal).HasName("PK__EmpresaL__C287B9BBE967FFC6");

            entity.Property(e => e.IdLocal).HasComment("LocalID");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdDireccion).HasComment("Dirección ID");
            entity.Property(e => e.IdEmpresa).HasComment("Empresa ID");
            entity.Property(e => e.RazonSocial)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Razón Social");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdDireccionNavigation).WithMany(p => p.EmpresaLocal)
                .HasForeignKey(d => d.IdDireccion)
                .HasConstraintName("FK_EmpresaLocal_Direccion ");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.EmpresaLocal)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmpresaLocal_Empresa");
        });

        modelBuilder.Entity<EntidadFinanciera>(entity =>
        {
            entity.HasKey(e => e.IdEntidadFinanciera).HasName("PK__EntidadF__DB55A39A6075FF79");

            entity.Property(e => e.IdEntidadFinanciera).HasComment("Entidad Financiera ID");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .HasComment("Código");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<GrupoArticulo>(entity =>
        {
            entity.HasKey(e => e.IdGrupoArticulo).HasName("PK__GrupoArt__4F4878F364FA9B98");

            entity.Property(e => e.IdGrupoArticulo).HasComment("Grupo ID");
            entity.Property(e => e.Activo).HasComment("¿Es Activo?");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(2)
                .HasComment("Código");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Descripción");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__Menu__4D7EA8E1948D702D");

            entity.Property(e => e.IdMenu).HasComment("Menú ID");
            entity.Property(e => e.Controlador)
                .HasMaxLength(60)
                .HasComment("Controlador");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(60)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo)
                .HasDefaultValue(false)
                .HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.Icono)
                .HasMaxLength(60)
                .HasComment("Icono");
            entity.Property(e => e.IdMenuPadre).HasComment("Menú Padre ID");
            entity.Property(e => e.PaginaAccion)
                .HasMaxLength(60)
                .HasComment("Acción");
            entity.Property(e => e.UsuarioName)
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdMenuPadreNavigation).WithMany(p => p.InverseIdMenuPadreNavigation)
                .HasForeignKey(d => d.IdMenuPadre)
                .HasConstraintName("FK__Menu__IdMenuPadr__3F466844");
        });

        modelBuilder.Entity<Moneda>(entity =>
        {
            entity.HasKey(e => e.IdMoneda).HasName("PK__Moneda__AA690671E66820D7");

            entity.Property(e => e.IdMoneda).HasComment("Moneda ID");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdSunat)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("Código SUNAT");
            entity.Property(e => e.Simbolo)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("Símbolo");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<Proceso>(entity =>
        {
            entity.HasKey(e => e.IdProceso).HasName("PK__Proceso__036D0743DB112F31");

            entity.Property(e => e.IdProceso).HasComment("Proceso ID");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.Orden)
                .HasDefaultValue(90)
                .HasComment("Orden Presentación");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.IdProvincia).HasName("PK__Provinci__EED74455BCFE5223");

            entity.Property(e => e.IdProvincia).HasComment("Provincia ID");
            entity.Property(e => e.CodigoProvincia)
                .IsRequired()
                .HasMaxLength(4)
                .HasComment("Código");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdDepartamento).HasComment("Departamento ID");
            entity.Property(e => e.Provincia1)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Provincia")
                .HasColumnName("Provincia");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Provincia)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Provincia_Departamento");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C7C0FD168");

            entity.Property(e => e.IdRol).HasComment("Rol ID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(40)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioName)
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<RolMenu>(entity =>
        {
            entity.HasKey(e => e.IdRolMenu).HasName("PK__RolMenu__79F10105818100C0");

            entity.Property(e => e.IdRolMenu).HasComment("Rol Menú ID");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdMenu).HasComment("Menú ID");
            entity.Property(e => e.IdRol).HasComment("Rol ID");
            entity.Property(e => e.UsuarioName)
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.RolMenu)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK__RolMenu__IdMenu__47DBAE45");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.RolMenu)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__RolMenu__IdRol__46E78A0C");
        });

        modelBuilder.Entity<Socio>(entity =>
        {
            entity.HasKey(e => e.IdSocio).HasName("PK__Socio__863F0ECFAF0E438C");

            entity.Property(e => e.IdSocio).HasComment("Socio ID");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(100)
                .HasComment("Apellido Materno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(100)
                .HasComment("Apellido Paterno");
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .HasComment("Celular");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasComment("Email");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaBaja)
                .HasComment("Fecha Baja")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaInicioOperaciones)
                .HasComment("Fecha Inicio Operaciones")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdGrupoSocioNegocio).HasComment("Grupo Económico ID");
            entity.Property(e => e.IdTipoCalificacion).HasComment("Tipo Calificación ID");
            entity.Property(e => e.IdTipoDcmtoIdentidad).HasComment("Tipo Dcmto Identidad ID");
            entity.Property(e => e.IdTipoMotivoBaja).HasComment("Tipo Motivo Baja ID");
            entity.Property(e => e.IdTipoSocio).HasComment("Tipo Socio ID");
            entity.Property(e => e.IsAfectoPercepcion).HasComment("¿Afecto a Percepción?");
            entity.Property(e => e.IsAfectoRetencion).HasComment("¿Afecto a Retención?");
            entity.Property(e => e.IsBuenContribuyente).HasComment("¿Buen Contribuyente?");
            entity.Property(e => e.LimiteCredito)
                .HasComment("Límite Crédito")
                .HasColumnType("decimal(13, 2)");
            entity.Property(e => e.NumeroDcmtoIdentidad)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("Número Dcmto Identidad");
            entity.Property(e => e.PaginaWeb)
                .HasMaxLength(100)
                .HasComment("Página Web");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(100)
                .HasComment("Primer Nombre");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(100)
                .HasComment("Razón Social");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(100)
                .HasComment("Segundo Nombre");
            entity.Property(e => e.Sobregiro)
                .HasComment("Sobregiros")
                .HasColumnType("decimal(13, 2)");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasComment("Teléfono");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
            entity.Property(e => e.ZonaPostal)
                .HasMaxLength(10)
                .HasComment("Zona Postal");

            entity.HasOne(d => d.IdGrupoSocioNegocioNavigation).WithMany(p => p.InverseIdGrupoSocioNegocioNavigation)
                .HasForeignKey(d => d.IdGrupoSocioNegocio)
                .HasConstraintName("FK_Socio_Socio");

            entity.HasOne(d => d.IdTipoCalificacionNavigation).WithMany(p => p.Socio)
                .HasForeignKey(d => d.IdTipoCalificacion)
                .HasConstraintName("FK_Socio_TipoCalificacion");

            entity.HasOne(d => d.IdTipoDcmtoIdentidadNavigation).WithMany(p => p.Socio)
                .HasForeignKey(d => d.IdTipoDcmtoIdentidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Socio_TipoDocumentoIdentidad");

            entity.HasOne(d => d.IdTipoMotivoBajaNavigation).WithMany(p => p.Socio)
                .HasForeignKey(d => d.IdTipoMotivoBaja)
                .HasConstraintName("FK_Socio_TipoMotivoBaja");

            entity.HasOne(d => d.IdTipoSocioNavigation).WithMany(p => p.Socio)
                .HasForeignKey(d => d.IdTipoSocio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Socio_TipoSocio");
        });

        modelBuilder.Entity<SocioCuentaBanco>(entity =>
        {
            entity.HasKey(e => e.IdCuenta).HasName("PK__SocioCue__D41FD706DE4A4F05");

            entity.Property(e => e.IdCuenta).HasComment("Cuenta Bancaria ID");
            entity.Property(e => e.CodigoCuenta)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Código Cuenta");
            entity.Property(e => e.CodigoCuentaInterbancaria)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Código Cuenta Interbancaria");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdEntidadFinanciera).HasComment("Entidad Financiera ID");
            entity.Property(e => e.IdMoneda).HasComment("Moneda ID");
            entity.Property(e => e.IdSocio).HasComment("Socio ID");
            entity.Property(e => e.IdTipoCuentaBancaria).HasComment("Tipo Cta Bancaria ID");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdEntidadFinancieraNavigation).WithMany(p => p.SocioCuentaBanco)
                .HasForeignKey(d => d.IdEntidadFinanciera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SocioCuentaBanco_EntidadFinanciera");

            entity.HasOne(d => d.IdMonedaNavigation).WithMany(p => p.SocioCuentaBanco)
                .HasForeignKey(d => d.IdMoneda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SocioCuentaBanco_Moneda");

            entity.HasOne(d => d.IdSocioNavigation).WithMany(p => p.SocioCuentaBanco)
                .HasForeignKey(d => d.IdSocio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SocioCuentaBanco_Socio");

            entity.HasOne(d => d.IdTipoCuentaBancariaNavigation).WithMany(p => p.SocioCuentaBanco)
                .HasForeignKey(d => d.IdTipoCuentaBancaria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SocioCuentaBanco_TipoCuentaBancaria");
        });

        modelBuilder.Entity<SocioDireccion_>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PK__SocioDir__1F8E0C764F5B9946");

            entity.ToTable("SocioDireccion ");

            entity.Property(e => e.IdDireccion)
                .ValueGeneratedOnAdd()
                .HasComment("Dirección ID");
            entity.Property(e => e.DireccionId).HasComment("Dirección Asociada");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.SocioId).HasComment("Socio ID");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdDireccionNavigation).WithOne(p => p.SocioDireccion_)
                .HasForeignKey<SocioDireccion_>(d => d.IdDireccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SocioDireccion _Direccion ");

            entity.HasOne(d => d.Socio).WithMany(p => p.SocioDireccion_)
                .HasForeignKey(d => d.SocioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SocioDireccion _Socio");
        });

        modelBuilder.Entity<SocioFormaPago>(entity =>
        {
            entity.HasKey(e => e.IdFormaPago).HasName("PK__SocioFor__C777CA686CD95144");

            entity.Property(e => e.IdFormaPago).HasComment("Forma Pago ID");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdSocio).HasComment("Socio ID");
            entity.Property(e => e.IdTipoFormaPago).HasComment("Tipo Forma Pago ID");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdSocioNavigation).WithMany(p => p.SocioFormaPago)
                .HasForeignKey(d => d.IdSocio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SocioFormaPago_Socio");

            entity.HasOne(d => d.IdTipoFormaPagoNavigation).WithMany(p => p.SocioFormaPago)
                .HasForeignKey(d => d.IdTipoFormaPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SocioFormaPago_TipoFormaPago");
        });

        modelBuilder.Entity<SocioPrecioArticulo>(entity =>
        {
            entity.HasKey(e => e.IdPrecioArticulo).HasName("PK__SocioPre__8E303FC3A2C4CF67");

            entity.Property(e => e.IdPrecioArticulo).HasComment("Precio Artículo ID");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdArticulo).HasComment("Artículo ID");
            entity.Property(e => e.IdMoneda).HasComment("Moneda ID");
            entity.Property(e => e.IdSocio).HasComment("Socio ID");
            entity.Property(e => e.PrecioUnitario)
                .HasComment("Precio Unitario")
                .HasColumnType("decimal(19, 6)");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.SocioPrecioArticulo)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SocioPrecioArticulo_Articulo");

            entity.HasOne(d => d.IdMonedaNavigation).WithMany(p => p.SocioPrecioArticulo)
                .HasForeignKey(d => d.IdMoneda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SocioPrecioArticulo_Moneda");

            entity.HasOne(d => d.IdSocioNavigation).WithMany(p => p.SocioPrecioArticulo)
                .HasForeignKey(d => d.IdSocio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SocioPrecioArticulo_Socio");
        });

        modelBuilder.Entity<SubGrupoArticulo>(entity =>
        {
            entity.HasKey(e => e.IdSubGrupoArticulo).HasName("PK__SubGrupo__E5E25EE8FC44D89E");

            entity.Property(e => e.IdSubGrupoArticulo).HasComment("Sub Grupo ID");
            entity.Property(e => e.Activo).HasComment("¿Es Activo?");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(4)
                .HasComment("Código");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Descripción");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdGrupoArticulo).HasComment("Grupo ID");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdGrupoArticuloNavigation).WithMany(p => p.SubGrupoArticulo)
                .HasForeignKey(d => d.IdGrupoArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubGrupoArticulo_GrupoArticulo");
        });

        modelBuilder.Entity<SubTipoMovimiento>(entity =>
        {
            entity.HasKey(e => e.IdSubTipoMovimiento).HasName("PK__SubTipoM__4D17BB5DA70F2A03");

            entity.Property(e => e.IdSubTipoMovimiento).HasComment("Sub Tipo Movto ID");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdTipoMovimiento).HasComment("Tipo Movto ID");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdTipoMovimientoNavigation).WithMany(p => p.SubTipoMovimiento)
                .HasForeignKey(d => d.IdTipoMovimiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubTipoMovimiento_TipoMovimiento");
        });

        modelBuilder.Entity<SunatTipoDetraccion>(entity =>
        {
            entity.HasKey(e => e.IdTipoDetraccion).HasName("PK__SunatTip__452A3A5A6A2C0CD9");

            entity.Property(e => e.IdTipoDetraccion).HasComment("Tipo Detracción ID");
            entity.Property(e => e.Activo).HasComment("¿Es Activo?");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(10)
                .HasComment("Código");
            entity.Property(e => e.Condicion)
                .IsRequired()
                .HasMaxLength(5)
                .HasComment("Condición");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Descripción");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.Porcentaje)
                .HasComment("Porcentaje")
                .HasColumnType("decimal(13, 2)");
            entity.Property(e => e.Unidad)
                .IsRequired()
                .HasMaxLength(10)
                .HasComment("Unidad UIT - S/.");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Fecha");
            entity.Property(e => e.Valor)
                .HasComment("Valor")
                .HasColumnType("decimal(13, 2)");
        });

        modelBuilder.Entity<SunatTipoExistencia>(entity =>
        {
            entity.HasKey(e => e.IdTipoExistencia).HasName("PK__SunatTip__7DB4ACA3DA7020B4");

            entity.Property(e => e.IdTipoExistencia).HasComment("Tipo Existencia ID");
            entity.Property(e => e.Activo).HasComment("¿Es Activo?");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(10)
                .HasComment("Código");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Descripción");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Fecha");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.IdTicket).HasName("PK__Ticket__4B93C7E7866B54CC");

            entity.Property(e => e.IdTicket).HasComment("Ticket ID");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaPesaje)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Fecha Pesaje")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.GuiaRemisionNumero)
                .IsRequired()
                .HasMaxLength(10)
                .HasDefaultValue("")
                .HasComment("Nro Guía Remisión");
            entity.Property(e => e.GuiaRemisionSerie)
                .IsRequired()
                .HasMaxLength(10)
                .HasDefaultValue("")
                .HasComment("Serie Guía Remisión");
            entity.Property(e => e.IdAlmacenDestino).HasComment("Almacén Destino ID");
            entity.Property(e => e.IdAlmacenOrigen).HasComment("Almacén Origen ID");
            entity.Property(e => e.IdChofer).HasComment("Chofer ID");
            entity.Property(e => e.IdTipoMovimiento).HasComment("Tipo Movto ID");
            entity.Property(e => e.IdTransportista).HasComment("Transportista ID");
            entity.Property(e => e.IdVehiculo).HasComment("Vehículo ID");
            entity.Property(e => e.NumeroTicket).HasComment("NúmeroTicket");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasDefaultValue("Admin")
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdAlmacenDestinoNavigation).WithMany(p => p.TicketIdAlmacenDestinoNavigation)
                .HasForeignKey(d => d.IdAlmacenDestino)
                .HasConstraintName("FK_Ticket_Almacen1");

            entity.HasOne(d => d.IdAlmacenOrigenNavigation).WithMany(p => p.TicketIdAlmacenOrigenNavigation)
                .HasForeignKey(d => d.IdAlmacenOrigen)
                .HasConstraintName("FK_Ticket_Almacen");

            entity.HasOne(d => d.IdTipoMovimientoNavigation).WithMany(p => p.Ticket)
                .HasForeignKey(d => d.IdTipoMovimiento)
                .HasConstraintName("FK_Ticket_TipoMovimiento");
        });

        modelBuilder.Entity<TicketLog>(entity =>
        {
            entity.HasKey(e => e.IdTicketLog).HasName("PK__TicketLo__171B5A05CBCE26A5");

            entity.Property(e => e.IdTicketLog).HasComment("Ticket Log ID");
            entity.Property(e => e.DatoDecia)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasComment("Decía");
            entity.Property(e => e.DatoDice)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasComment("Dice");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdTicket).HasComment("Ticket ID");
            entity.Property(e => e.MotivoCrud)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasComment("Motivo del Log");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasDefaultValue("Admin")
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdTicketNavigation).WithMany(p => p.TicketLog)
                .HasForeignKey(d => d.IdTicket)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketLog_Ticket");
        });

        modelBuilder.Entity<TicketPesajes>(entity =>
        {
            entity.HasKey(e => e.IdTicketPesaje).HasName("PK__TicketPe__CB1278780CA17EA6");

            entity.Property(e => e.IdTicketPesaje).HasComment("Ticket Pesaje ID");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdArticulo).HasComment("Artículo ID");
            entity.Property(e => e.IdTicket).HasComment("Ticket ID");
            entity.Property(e => e.IdUnidad).HasComment("Unidad Pesaje");
            entity.Property(e => e.NumeroPesaje)
                .HasDefaultValue(1)
                .HasComment("Nro Pesaje");
            entity.Property(e => e.PesoBalanza)
                .HasComment("Peso Balanza")
                .HasColumnType("decimal(21, 3)");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasDefaultValue("Admin")
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.TicketPesajes)
                .HasForeignKey(d => d.IdArticulo)
                .HasConstraintName("FK_TicketPesajes_Articulo");

            entity.HasOne(d => d.IdTicketNavigation).WithMany(p => p.TicketPesajes)
                .HasForeignKey(d => d.IdTicket)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketPesajes_Ticket");
        });

        modelBuilder.Entity<TipoAlmacen>(entity =>
        {
            entity.HasKey(e => e.IdTipoAlmacen).HasName("PK__TipoAlma__49824024FDF3DCFD");

            entity.Property(e => e.IdTipoAlmacen).HasComment("Tipo Almacén ID");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("Código");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<TipoArticulo>(entity =>
        {
            entity.HasKey(e => e.IdTipoArticulo).HasName("PK__TipoArti__04163B48AD82EF5B");

            entity.Property(e => e.IdTipoArticulo).HasComment("Tipo Artículo ID");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<TipoCalificacion>(entity =>
        {
            entity.HasKey(e => e.IdTipoCalificacion).HasName("PK__TipoCali__10AF792EC34BF66B");

            entity.Property(e => e.IdTipoCalificacion).HasComment("Tipo Calificación ID");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(5)
                .HasComment("Código");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<TipoCuentaBancaria>(entity =>
        {
            entity.HasKey(e => e.IdTipoCuentaBancaria).HasName("PK__TipoCuen__D6BB8B361B2320C8");

            entity.Property(e => e.IdTipoCuentaBancaria).HasComment("Tipo Cta Bancaria ID");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(10)
                .HasComment("Código");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<TipoDireccion>(entity =>
        {
            entity.HasKey(e => e.IdTipoDireccion).HasName("PK__TipoDire__1279FA0E5124EE96");

            entity.Property(e => e.IdTipoDireccion).HasComment("Tipo Direccón ID");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.Orden).HasComment("Orden Presentación");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<TipoDocumentoIdentidad>(entity =>
        {
            entity.HasKey(e => e.IdTipoDcmtoIdentidad).HasName("PK__TipoDocu__54C3516720DF199A");

            entity.Property(e => e.IdTipoDcmtoIdentidad).HasComment("Tipo Dcmto ID");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdCodigoSunat)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("Código SUNAT");
            entity.Property(e => e.Orden).HasComment("Orden Presentación");
            entity.Property(e => e.Simbolo)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("Símbolo");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<TipoFlete>(entity =>
        {
            entity.HasKey(e => e.IdFlete).HasName("PK__TipoFlet__F7F9EB921E3E2B5F");

            entity.Property(e => e.IdFlete).HasComment("Tipo Flete ID");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("Código");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(40)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasDefaultValue("Admin")
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<TipoFormaPago>(entity =>
        {
            entity.HasKey(e => e.IdTipoFormaPago).HasName("PK__TipoForm__F35DA87EF99BEF18");

            entity.Property(e => e.IdTipoFormaPago).HasComment("Tipo Forma Pago ID");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("Código");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Descripción");
            entity.Property(e => e.DiasDePago).HasComment("Días de Pago");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<TipoMotivoBaja>(entity =>
        {
            entity.HasKey(e => e.IdTipoMotivoBaja).HasName("PK__TipoMoti__E76F77ED64864ACD");

            entity.Property(e => e.IdTipoMotivoBaja).HasComment("Tipo Motivo Baja ID");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.Orden).HasComment("Orden Presentación");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<TipoMovimiento>(entity =>
        {
            entity.HasKey(e => e.IdTipoMovimiento).HasName("PK__TipoMovi__820D7FC23A857CEE");

            entity.Property(e => e.IdTipoMovimiento).HasComment("Tipo Movto ID");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<TipoSocio>(entity =>
        {
            entity.HasKey(e => e.IdTipoSocio).HasName("PK__TipoSoci__F3E7DE983E5EBBE1");

            entity.Property(e => e.IdTipoSocio).HasComment("Tipo Socio ID");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("Código");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.Orden)
                .HasDefaultValue(90)
                .HasComment("Orden Presentación");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<TipoValorizacion>(entity =>
        {
            entity.HasKey(e => e.IdTipoValorizacion).HasName("PK__TipoValo__952EC84A7D2986A9");

            entity.Property(e => e.IdTipoValorizacion).HasComment("Tipo Valorización ID");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(10)
                .HasComment("Código");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.Orden).HasComment("Orden Presentación");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<TipoVia>(entity =>
        {
            entity.HasKey(e => e.IdTipoVia).HasName("PK__TipoVia__3CB3592BE9103B48");

            entity.Property(e => e.IdTipoVia).HasComment("Tipo Via ID");
            entity.Property(e => e.CodigoTipoVia)
                .IsRequired()
                .HasMaxLength(10)
                .HasComment("Código");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.Orden).HasComment("Orden Presentación");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<TipoZona>(entity =>
        {
            entity.HasKey(e => e.IdTipoZona).HasName("PK__TipoZona__C23AC18D2F0F54C4");

            entity.Property(e => e.IdTipoZona).HasComment("Tipo Zona ID");
            entity.Property(e => e.CodigoTipoZona)
                .IsRequired()
                .HasMaxLength(10)
                .HasComment("Código TipoZona");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.Orden).HasComment("Orden Presentación");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<UnidadMedida>(entity =>
        {
            entity.HasKey(e => e.IdUnidad).HasName("PK__UnidadMe__437725E6965CC7D4");

            entity.Property(e => e.IdUnidad).HasComment("Unidad ID");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Descripción");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdSunat)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("Código SUNAT");
            entity.Property(e => e.Simbolo)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("Símbolo");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97EEE38AE0");

            entity.Property(e => e.IdUsuario).HasComment("Usuario ID");
            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .HasComment("Clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasComment("Correo");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdRol).HasComment("Rol ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .HasComment("Nombre");
            entity.Property(e => e.NombreFoto)
                .HasMaxLength(100)
                .HasComment("Nombre Foto");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .HasComment("Celular");
            entity.Property(e => e.UrlFoto)
                .HasMaxLength(500)
                .HasComment("Url Foto");
            entity.Property(e => e.UsuarioName)
                .HasMaxLength(60)
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__IdRol__4BAC3F29");
        });

        modelBuilder.Entity<Vehiculos>(entity =>
        {
            entity.HasKey(e => e.IdVehiculo).HasName("PK__Vehiculo__70861215CDCFC43C");

            entity.Property(e => e.IdVehiculo).HasComment("Vehículo ID");
            entity.Property(e => e.Certificado)
                .IsRequired()
                .HasMaxLength(40)
                .HasDefaultValue("")
                .HasComment("Certificado Inscripción");
            entity.Property(e => e.Color)
                .IsRequired()
                .HasMaxLength(40)
                .HasDefaultValue("")
                .HasComment("Color");
            entity.Property(e => e.EsActivo).HasComment("¿Es Activo?");
            entity.Property(e => e.EsDeEmpresa).HasComment("¿Es de la Empresa?");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Auditoría Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.IdFlete).HasComment("Flete ID");
            entity.Property(e => e.Marca)
                .IsRequired()
                .HasMaxLength(40)
                .HasDefaultValue("")
                .HasComment("Marca");
            entity.Property(e => e.Modelo)
                .IsRequired()
                .HasMaxLength(40)
                .HasDefaultValue("")
                .HasComment("Modelo");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(40)
                .HasComment("Nombre");
            entity.Property(e => e.PesoKg)
                .HasComment("Peso en Kg")
                .HasColumnType("decimal(13, 2)");
            entity.Property(e => e.Placa)
                .IsRequired()
                .HasMaxLength(40)
                .HasDefaultValue("")
                .HasComment("Placa");
            entity.Property(e => e.UsuarioName)
                .IsRequired()
                .HasMaxLength(60)
                .HasDefaultValue("Admin")
                .HasComment("Auditoría Usuario");

            entity.HasOne(d => d.IdFleteNavigation).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.IdFlete)
                .HasConstraintName("FK_Vehiculos_TipoFlete");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}