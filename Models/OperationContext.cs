using AutoVentas_Back.Models;
using Microsoft.EntityFrameworkCore;

public class OperationContext : ModelContext
{
    public OperationContext(DbContextOptions<OperationContext> options)
    : base(options)
    {
    }

}
