using System;
using System.Collections.Generic;

namespace AutoVentas_Back.Models;

public partial class Ubicacion
{
    public decimal CodUbicacion { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
