
using AutoVentas_Back.Repositories;
using AutoVentas_Back.Services;
using AutoVentas_Back.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Registrar los repositorios y servicios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();
builder.Services.AddScoped<IUbicacionRepository, UbicacionRepository>();
builder.Services.AddScoped<IStatusVentaRepository, StatusVentaRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IVentaRepository, VentaRepository>();



// Registrar Sercicios
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<IUbicacionService, UbicacionService>();
builder.Services.AddScoped<IStatusVentaService, StatusVentaService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IVentaService, VentaService>();



//Encripcion de String 
var configuration = builder.Configuration;
string encryptionKey = configuration["EncryptionKey"];

if (string.IsNullOrWhiteSpace(encryptionKey))
{
    throw new InvalidOperationException("La clave de encriptaci�n no est� definida. Verifica las variables de entorno.");
}

// Desencriptar las cadenas de conexi�n
var QueryUserConnection = Encription.DecryptConnectionString(configuration.GetConnectionString("OracleQueryUser"), encryptionKey);
var OperationUserConnection = Encription.DecryptConnectionString(configuration.GetConnectionString("OracleOperationUser"), encryptionKey);

// Registrar QueryContext para consultas
builder.Services.AddDbContext<QueryContext>(options =>
    options.UseOracle(QueryUserConnection));

// Registrar OperationContext para operaciones
builder.Services.AddDbContext<OperationContext>(options =>
    options.UseOracle(OperationUserConnection));


/*
   "ConnectionStrings": {
    "OracleQueryUser": "User Id=USR_DESAWEB;Password=DESAWEB123;Data Source=34.172.95.117:1521/xe;",
    "OracleOperationUser": "User Id=OP_DESAWEB;Password=DESAWEB123;Data Source=34.172.95.117:1521/xe;"
rop+MXM0MOJfISIgAkHzbm3NbINDpbZgyceVlmfoWJxyZaAANdgFu6VEVsUWRaNfe85FKJ5QHu0j0Rs4nWSwVGurPEVgk5FAtzbDKyIQf6iPt34thWOZ87BP03TmrElW
SukLJ6WEc+VpxGkirGss30PByKNEVKgTtdWu9r6G8G/7l06QnnEOzEzVP+/6aUilXOfl4tHVBFUtJjQBe4LRLRDs+RjOo3rCIlcZHN+xmFBnsSEEUL+qXD9F0nyBfMeO

  },

// Registrar QueryContext para consultas
builder.Services.AddDbContext<QueryContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleQueryUser")));

// Registrar OperationContext para operaciones
builder.Services.AddDbContext<OperationContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleOperationUser")));
 */

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
// Mapea la ruta ra�z ("/") para devolver la versi�n
app.MapGet("/", () => Results.Json(new { Version = "1.0.0" }));
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
