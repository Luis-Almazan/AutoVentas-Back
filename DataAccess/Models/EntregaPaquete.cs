using System;
using System.Collections.Generic;

namespace AutoVentas_Back.DataAccess.Models;

public partial class EntregaPaquete
{
    public decimal CodEntrega { get; set; }

    public string? Descripcion { get; set; }

    public string? Observaciones { get; set; }

    public decimal CodCliente { get; set; }

    public decimal CodVenta { get; set; }

    public virtual Cliente CodClienteNavigation { get; set; } = null!;

    public virtual Ventum CodVentaNavigation { get; set; } = null!;
}
