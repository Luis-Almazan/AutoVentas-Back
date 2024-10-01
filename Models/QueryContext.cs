using AutoVentas_Back.Models;
using Microsoft.EntityFrameworkCore;

public class QueryContext : ModelContext
{
    public QueryContext(DbContextOptions<QueryContext> options)
    : base(options)
    {
    }

    // Aquí puedes poner cualquier lógica adicional para este contexto si es necesario
}
