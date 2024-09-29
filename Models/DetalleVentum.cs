using System;
using System.Collections.Generic;

namespace AutoVentas_Back.Models;

public partial class DetalleVentum
{
    public decimal CodDetalleVenta { get; set; }

    public decimal CodVenta { get; set; }

    public decimal CodProducto { get; set; }

    public decimal? Cantidad { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? Status { get; set; }

    public decimal CodDevolucionProducto { get; set; }

    public virtual DevolucionProducto CodDevolucionProductoNavigation { get; set; } = null!;

    public virtual Producto CodProductoNavigation { get; set; } = null!;

    public virtual Ventum CodVentaNavigation { get; set; } = null!;
}
