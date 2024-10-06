using System;
using System.Collections.Generic;

namespace AutoVentas_Back.DataAccess.Models;

public partial class Categorium
{
    public decimal CodCategoria { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }
}
