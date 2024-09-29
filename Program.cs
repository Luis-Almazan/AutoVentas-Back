using AutoVentas_Back.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registrar ModelContext como el contexto base
builder.Services.AddDbContext<ModelContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleQueryUser")));


// Registrar QueryContext para consultas
builder.Services.AddDbContext<QueryContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleQueryUser")));

// Registrar OperationContext para operaciones
builder.Services.AddDbContext<OperationContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleOperationUser")));



// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
// Mapea la ruta raíz ("/") para devolver la versión
app.MapGet("/", () => Results.Json(new { Version = "1.0.0" }));
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
