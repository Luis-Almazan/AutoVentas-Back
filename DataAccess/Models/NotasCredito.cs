using System;
using System.Collections.Generic;

namespace AutoVentas_Back.DataAccess.Models;

public partial class NotasCredito
{
    public decimal CodNotaCredito { get; set; }

    public decimal CodCliente { get; set; }

    public string? TipoNota { get; set; }

    public DateTime? FechaNota { get; set; }

    public decimal? Total { get; set; }

    public decimal CodVenta { get; set; }

    public virtual ICollection<AnulacionVenta> AnulacionVenta { get; set; } = new List<AnulacionVenta>();

    public virtual Cliente CodClienteNavigation { get; set; } = null!;

    public virtual Ventum CodVentaNavigation { get; set; } = null!;

    public virtual ICollection<DevolucionProducto> DevolucionProductos { get; set; } = new List<DevolucionProducto>();
}
