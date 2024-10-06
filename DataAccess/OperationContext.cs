using AutoVentas_Back.DataAccess;
using Microsoft.EntityFrameworkCore;

public class OperationContext : ModelContext
{
    public OperationContext(DbContextOptions<OperationContext> options)
    : base(options)
    {
    }

}
