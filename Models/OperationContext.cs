using AutoVentas_Back.Models;
using Microsoft.EntityFrameworkCore;

public class OperationContext : ModelContext
{
    public OperationContext(DbContextOptions<ModelContext> options)
    : base(options)
    {
    }

}
