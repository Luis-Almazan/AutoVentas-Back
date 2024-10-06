using System;
using System.Collections.Generic;

namespace AutoVentas_Back.DataAccess.Models;

public partial class StatusVentum
{
    public decimal CodVenta { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual Ventum? Ventum { get; set; }
}
