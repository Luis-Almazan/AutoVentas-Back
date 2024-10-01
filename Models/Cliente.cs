using System;
using System.Collections.Generic;

namespace AutoVentas_Back.Models;

public partial class Cliente
{
    public decimal CodCliente { get; set; }

    public string? PrimerNombre { get; set; }

    public string? SegundoNombre { get; set; }

    public string? PrimerApellido { get; set; }

    public string? SegundoApellido { get; set; }

    public decimal? Nit { get; set; }

    public string? DireccionCliente { get; set; }

    public decimal CategoriaCliente { get; set; }

    public decimal Status { get; set; }

    public virtual ICollection<EntregaPaquete> EntregaPaquetes { get; set; } = new List<EntregaPaquete>();

    public virtual ICollection<NotasCredito> NotasCreditos { get; set; } = new List<NotasCredito>();

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
