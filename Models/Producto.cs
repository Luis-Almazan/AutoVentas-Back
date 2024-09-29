using System;
using System.Collections.Generic;

namespace AutoVentas_Back.Models;

public partial class Producto
{
    public decimal CodProducto { get; set; }

    public string? Descripcion { get; set; }

    public decimal CodProveedor { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public decimal Ubicacion { get; set; }

    public decimal? Existencia { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Status { get; set; }

    public virtual Proveedor CodProveedorNavigation { get; set; } = null!;

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual Ubicacion UbicacionNavigation { get; set; } = null!;
}
