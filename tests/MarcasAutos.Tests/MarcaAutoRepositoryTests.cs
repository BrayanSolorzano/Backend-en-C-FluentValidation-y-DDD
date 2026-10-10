using System.Linq;
using System.Threading.Tasks;
using MarcasAutos.Domain.Entities;
using MarcasAutos.Infrastructure.Data;
using MarcasAutos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MarcasAutos.Tests.Repositories;

public class MarcaAutoRepositoryTests
{
    private readonly DbContextOptions<AppDbContext> _dbContextOptions;

    public MarcaAutoRepositoryTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllEntities()
    {
        // Arrange
        using var context = new AppDbContext(_dbContextOptions);
        context.MarcasAutos.Add(new MarcaAuto("Kia", "Corea del Sur"));
        context.MarcasAutos.Add(new MarcaAuto("Hyundai", "Corea del Sur"));
        await context.SaveChangesAsync();

        var repository = new MarcaAutoRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        var marcasList = result.ToList();
        Assert.Equal(2, marcasList.Count);
        Assert.Contains(marcasList, m => m.Nombre == "Kia");
        Assert.Contains(marcasList, m => m.Nombre == "Hyundai");
    }

    [Fact]
    public async Task GetAllAsync_ReturnsEmptyList_WhenNoData()
    {
        // Arrange
        using var context = new AppDbContext(_dbContextOptions);
        var repository = new MarcaAutoRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
