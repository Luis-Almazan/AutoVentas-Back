using System;
using System.Collections.Generic;

namespace AutoVentas_Back.DataAccess.Models;

public partial class Ventum
{
    public decimal CodVenta { get; set; }

    public decimal CodCliente { get; set; }

    public DateTime? FechaVenta { get; set; }

    public decimal? TotalVenta { get; set; }

    public decimal StatusVenta { get; set; }

    public decimal? CodAnulacionVenta { get; set; }

    public virtual ICollection<AnulacionVenta>? AnulacionVenta { get; set; } = new List<AnulacionVenta>();

    public virtual Cliente? CodClienteNavigation { get; set; } = null!;

    public virtual StatusVentum? CodVentaNavigation { get; set; } = null!;

    public virtual ICollection<DetalleVentum>? DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual ICollection<EntregaPaquete>? EntregaPaquetes { get; set; } = new List<EntregaPaquete>();

    public virtual ICollection<NotasCredito> NotasCreditos { get; set; } = new List<NotasCredito>();
}
