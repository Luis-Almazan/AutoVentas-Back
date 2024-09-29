using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AutoVentas_Back.Models;

public partial class ModelContext : DbContext
{

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Solo entra aquí si no se configuró la cadena de conexión, pero ya lo hacemos desde `Startup` o `Program`.
            throw new InvalidOperationException("The context was not configured.");
        }
    }
    public ModelContext()
    {
    }

    public virtual DbSet<AnulacionVenta> AnulacionVentas { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }

    public virtual DbSet<DevolucionProducto> DevolucionProductos { get; set; }

    public virtual DbSet<EntregaPaquete> EntregaPaquetes { get; set; }

    public virtual DbSet<NotasCredito> NotasCreditos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<StatusVentum> StatusVenta { get; set; }

    public virtual DbSet<Ubicacion> Ubicacions { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("DESAWEB")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<AnulacionVenta>(entity =>
        {
            entity.HasKey(e => e.CodAnulacion).HasName("ANULACION_VENTAS_PK");

            entity.ToTable("ANULACION_VENTAS");

            entity.Property(e => e.CodAnulacion)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_ANULACION");
            entity.Property(e => e.CodNotaCredito)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_NOTA_CREDITO");
            entity.Property(e => e.CodVenta)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_VENTA");
            entity.Property(e => e.MotivoAnulacion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("MOTIVO_ANULACION");

            entity.HasOne(d => d.CodNotaCreditoNavigation).WithMany(p => p.AnulacionVenta)
                .HasForeignKey(d => d.CodNotaCredito)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ANULACIONVENTA_NOTACREDITO_FK");

            entity.HasOne(d => d.CodVentaNavigation).WithMany(p => p.AnulacionVenta)
                .HasForeignKey(d => d.CodVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ANULACION_VENTAS_VENTA_FK");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.CodCategoria).HasName("CATEGORIA_PK");

            entity.ToTable("CATEGORIA");

            entity.Property(e => e.CodCategoria)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_CATEGORIA");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.CodCliente).HasName("CLIENTE_PK");

            entity.ToTable("CLIENTE");

            entity.Property(e => e.CodCliente)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_CLIENTE");
            entity.Property(e => e.CategoriaCliente)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CATEGORIA_CLIENTE");
            entity.Property(e => e.DireccionCliente)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DIRECCION_CLIENTE");
            entity.Property(e => e.Nit)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("NIT");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("PRIMER_APELLIDO");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("PRIMER_NOMBRE");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("SEGUNDO_APELLIDO");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("SEGUNDO_NOMBRE");
            entity.Property(e => e.Status)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity.HasKey(e => e.CodDetalleVenta).HasName("DETALLE_VENTA_PK");

            entity.ToTable("DETALLE_VENTA");

            entity.Property(e => e.CodDetalleVenta)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_DETALLE_VENTA");
            entity.Property(e => e.Cantidad)
                .HasColumnType("NUMBER")
                .HasColumnName("CANTIDAD");
            entity.Property(e => e.CodDevolucionProducto)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_DEVOLUCION_PRODUCTO");
            entity.Property(e => e.CodProducto)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_PRODUCTO");
            entity.Property(e => e.CodVenta)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_VENTA");
            entity.Property(e => e.Status)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("STATUS");
            entity.Property(e => e.Subtotal)
                .HasColumnType("NUMBER")
                .HasColumnName("SUBTOTAL");

            entity.HasOne(d => d.CodDevolucionProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.CodDevolucionProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DETALLEVENTA_DEVOLUCIONPRODUCTO_FK");

            entity.HasOne(d => d.CodProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.CodProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DETALLE_VENTA_PRODUCTO_FK");

            entity.HasOne(d => d.CodVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.CodVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DETALLE_VENTA_VENTA_FK");
        });

        modelBuilder.Entity<DevolucionProducto>(entity =>
        {
            entity.HasKey(e => e.CodDevolucion).HasName("DEVOLUCION_PRODUCTOS_PK");

            entity.ToTable("DEVOLUCION_PRODUCTOS");

            entity.Property(e => e.CodDevolucion)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_DEVOLUCION");
            entity.Property(e => e.Cantidad)
                .HasColumnType("NUMBER")
                .HasColumnName("CANTIDAD");
            entity.Property(e => e.CodNotaCredito)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_NOTA_CREDITO");
            entity.Property(e => e.MotivoDevolucion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("MOTIVO_DEVOLUCION");

            entity.HasOne(d => d.CodNotaCreditoNavigation).WithMany(p => p.DevolucionProductos)
                .HasForeignKey(d => d.CodNotaCredito)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DEVOLUCIONPRODUCTO_NOTACREDITO_FK");
        });

        modelBuilder.Entity<EntregaPaquete>(entity =>
        {
            entity.HasKey(e => e.CodEntrega).HasName("ENTREGA_PAQUETE_PK");

            entity.ToTable("ENTREGA_PAQUETE");

            entity.Property(e => e.CodEntrega)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_ENTREGA");
            entity.Property(e => e.CodCliente)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_CLIENTE");
            entity.Property(e => e.CodVenta)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_VENTA");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("OBSERVACIONES");

            entity.HasOne(d => d.CodClienteNavigation).WithMany(p => p.EntregaPaquetes)
                .HasForeignKey(d => d.CodCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ENTREGA_PAQUETE_CLIENTE_FK");

            entity.HasOne(d => d.CodVentaNavigation).WithMany(p => p.EntregaPaquetes)
                .HasForeignKey(d => d.CodVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ENTREGA_PAQUETE_VENTA_FK");
        });

        modelBuilder.Entity<NotasCredito>(entity =>
        {
            entity.HasKey(e => e.CodNotaCredito).HasName("NOTAS_CREDITO_PK");

            entity.ToTable("NOTAS_CREDITO");

            entity.Property(e => e.CodNotaCredito)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_NOTA_CREDITO");
            entity.Property(e => e.CodCliente)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_CLIENTE");
            entity.Property(e => e.CodVenta)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_VENTA");
            entity.Property(e => e.FechaNota)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_NOTA");
            entity.Property(e => e.TipoNota)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TIPO_NOTA");
            entity.Property(e => e.Total)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTAL");

            entity.HasOne(d => d.CodClienteNavigation).WithMany(p => p.NotasCreditos)
                .HasForeignKey(d => d.CodCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("NOTAS_CREDITO_CLIENTE_FK");

            entity.HasOne(d => d.CodVentaNavigation).WithMany(p => p.NotasCreditos)
                .HasForeignKey(d => d.CodVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("NOTAS_CREDITO_VENTA_FK");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.CodProducto).HasName("PRODUCTO_PK");

            entity.ToTable("PRODUCTO");

            entity.Property(e => e.CodProducto)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_PRODUCTO");
            entity.Property(e => e.CodProveedor)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_PROVEEDOR");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Existencia)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EXISTENCIA");
            entity.Property(e => e.FechaVencimiento)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_VENCIMIENTO");
            entity.Property(e => e.Precio)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PRECIO");
            entity.Property(e => e.Status)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("STATUS");
            entity.Property(e => e.Ubicacion)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("UBICACION");

            entity.HasOne(d => d.CodProveedorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CodProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PRODUCTO_PROVEEDOR_FK");

            entity.HasOne(d => d.UbicacionNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Ubicacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PRODUCTO_UBICACION_FK");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.CodProveedor).HasName("PROVEEDOR_PK");

            entity.ToTable("PROVEEDOR");

            entity.Property(e => e.CodProveedor)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_PROVEEDOR");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<StatusVentum>(entity =>
        {
            entity.HasKey(e => e.CodVenta).HasName("STATUS_VENTA_PK");

            entity.ToTable("STATUS_VENTA");

            entity.Property(e => e.CodVenta)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_VENTA");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<Ubicacion>(entity =>
        {
            entity.HasKey(e => e.CodUbicacion).HasName("UBICACION_PK");

            entity.ToTable("UBICACION");

            entity.Property(e => e.CodUbicacion)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_UBICACION");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.CodVenta).HasName("VENTA_PK");

            entity.ToTable("VENTA");

            entity.Property(e => e.CodVenta)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_VENTA");
            entity.Property(e => e.CodAnulacionVenta)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_ANULACION_VENTA");
            entity.Property(e => e.CodCliente)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COD_CLIENTE");
            entity.Property(e => e.FechaVenta)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_VENTA");
            entity.Property(e => e.StatusVenta)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("STATUS_VENTA");
            entity.Property(e => e.TotalVenta)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTAL_VENTA");

            entity.HasOne(d => d.CodClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.CodCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("VENTA_CLIENTE_FK");

            entity.HasOne(d => d.CodVentaNavigation).WithOne(p => p.Ventum)
                .HasForeignKey<Ventum>(d => d.CodVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("VENTA_STATUS_VENTA_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
