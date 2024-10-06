using System;
using System.Collections.Generic;

namespace AutoVentas_Back.DataAccess.Models;

public partial class AnulacionVenta
{
    public decimal CodAnulacion { get; set; }

    public decimal CodNotaCredito { get; set; }

    public decimal CodVenta { get; set; }

    public string? MotivoAnulacion { get; set; }

    public virtual NotasCredito CodNotaCreditoNavigation { get; set; } = null!;

    public virtual Ventum CodVentaNavigation { get; set; } = null!;
}
