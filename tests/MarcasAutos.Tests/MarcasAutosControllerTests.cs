using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using MarcasAutos.API.Controllers;
using MarcasAutos.Application.DTOs;
using MarcasAutos.Application.Services;
using MarcasAutos.Domain.Entities;
using MarcasAutos.Infrastructure.Data;
using MarcasAutos.Infrastructure.Repositories;

namespace MarcasAutos.Tests.Controllers;

public class MarcasAutosControllerTests
{
    [Fact]
    public async Task Get_ReturnsOkResult_WithListOfMarcas()
    {
        // Inicio
        // Configurar en memoria Database
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "MarcasAutosTestDb")
            .Options;

        using var context = new AppDbContext(options);
        
        // Agregar datos de prueba (Data Seed)
        if (!context.MarcasAutos.Any())
        {
            context.MarcasAutos.AddRange(
                new MarcaAuto("Toyota", "Japón"),
                new MarcaAuto("Ford", "Estados Unidos"),
                new MarcaAuto("Volkswagen", "Alemania")
            );
            await context.SaveChangesAsync();
        }

        var repository = new MarcaAutoRepository(context);
        var service = new MarcaAutoService(repository);
        var controller = new MarcasAutosController(service);

        // Act
        var result = await controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var marcas = Assert.IsAssignableFrom<IEnumerable<MarcaAutoDto>>(okResult.Value);
        Assert.Equal(3, marcas.Count());
        Assert.Contains(marcas, m => m.Nombre == "Toyota");
        Assert.Contains(marcas, m => m.Nombre == "Ford");
        Assert.Contains(marcas, m => m.Nombre == "Volkswagen");
    }
}
