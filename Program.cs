var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Oracle_Conexion");

//builder.Services.AddDbContext<ModelContext>(options =>
//    options.UseOracle(connectionString));



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
