using FluentValidation;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MarcasAutos.Application.Services;
using MarcasAutos.Application.Validators;
using MarcasAutos.Domain.Repositories;
using MarcasAutos.Infrastructure.Data;
using MarcasAutos.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// configurar la cadena de conexión a PostgreSQL
// configurar la cadena de conexión a PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IMarcaAutoRepository, MarcaAutoRepository>();
builder.Services.AddScoped<IMarcaAutoService, MarcaAutoService>();

// configurar FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<MarcaAutoDtoValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

// Auto-migrar a Docker con Retry Logic
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var maxRetries = 5;
    var currentRetry = 0;
    var delay = TimeSpan.FromSeconds(3);

    while (currentRetry < maxRetries)
    {
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            if (context.Database.IsRelational())
            {
                logger.LogInformation("Intentando migrar database...");
                context.Database.Migrate();
                logger.LogInformation("Database migrada correctamente.");
                break; // Éxito, salir del loop
            }
        }
        catch (Exception ex)
        {
            currentRetry++;
            logger.LogWarning(ex, $" error ocurrio mientras se migraba la database. reintentar {currentRetry}/{maxRetries} in {delay.TotalSeconds} segundos...");
            if (currentRetry >= maxRetries)
            {
                logger.LogError(ex, "max intentos.");
            }
            Thread.Sleep(delay);
        }
    }
}

// Documentación interactiva de la API (Scalar / OpenAPI)
app.MapOpenApi();
app.MapScalarApiReference();

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
