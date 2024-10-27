using System;
using System.Collections.Generic;

namespace AutoVentas_Back.DataAccess.Models;

public partial class Bitacora
{
    public decimal IdBitacora { get; set; }

    public string? TablaNombre { get; set; }

    public string? Operacion { get; set; }

    public decimal? CodRegistro { get; set; }

    public string? Usuario { get; set; }

    public DateTime? FechaOperacion { get; set; }

    public string? Descripcion { get; set; }
}
