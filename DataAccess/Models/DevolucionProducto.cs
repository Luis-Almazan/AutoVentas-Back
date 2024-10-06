using System;
using System.Collections.Generic;

namespace AutoVentas_Back.DataAccess.Models;

public partial class DevolucionProducto
{
    public decimal CodDevolucion { get; set; }

    public decimal CodNotaCredito { get; set; }

    public decimal? Cantidad { get; set; }

    public string? MotivoDevolucion { get; set; }

    public virtual NotasCredito CodNotaCreditoNavigation { get; set; } = null!;

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();
}
