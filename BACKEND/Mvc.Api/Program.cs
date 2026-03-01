using DbModel.demoDb;
using Microsoft.EntityFrameworkCore;
using Mvc.Bussnies.Persona;
using Mvc.Bussnies.PersonaTipoDocumentoB;
using Mvc.Repository.PersonaRepo.Contratos;
using Mvc.Repository.PersonaRepo.Implementacion;
using Mvc.Repository.PersonaTipoDocumentoRepo.Contratos;
using Mvc.Repository.PersonaTipoDocumentoRepo.Implementacion;
using Mvc.Bussnies.casa;
using Mvc.Repository.casa;
using Mvc.Bussnies.vehiculo;
using Mvc.Repository.vehiculo;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configurar la conexión a la base de datos
builder.Services.AddDbContext<_demoContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("demoDb");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Inyección de dependencias - Repositories
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IPersonaBussnies, PersonaBussnies>();

builder.Services.AddScoped<IPersonaTipoDocumentoRepository, PersonaTipoDocumentoRepository>();
builder.Services.AddScoped<IPersonaTipoDocumentoBussnies, PersonaTipoDocumentoBussnies>();

builder.Services.AddScoped<ICasaBussnies, CasaBussnies>();
builder.Services.AddScoped<ICasaRepository, CasaRepository>();
builder.Services.AddScoped<IVehiculoBussnies, VehiculoBussnies>();
builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();
// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
