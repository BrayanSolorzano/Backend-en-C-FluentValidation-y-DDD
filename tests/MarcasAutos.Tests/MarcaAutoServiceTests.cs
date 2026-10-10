using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarcasAutos.Application.Services;
using MarcasAutos.Domain.Entities;
using MarcasAutos.Domain.Repositories;
using Moq;
using Xunit;

namespace MarcasAutos.Tests.Services;

public class MarcaAutoServiceTests
{
    private readonly Mock<IMarcaAutoRepository> _mockRepo;
    private readonly MarcaAutoService _service;

    public MarcaAutoServiceTests()
    {
        _mockRepo = new Mock<IMarcaAutoRepository>();
        _service = new MarcaAutoService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllMarcasAsync_ReturnsMappedDtos()
    {
        // Arrange
        var mockData = new List<MarcaAuto>
        {
            new MarcaAuto("Honda", "Japon"),
            new MarcaAuto("BMW", "Alemania")
        };
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(mockData);

        // Act
        var result = await _service.GetAllMarcasAsync();

        // Assert
        Assert.NotNull(result);
        var dtos = result.ToList();
        Assert.Equal(2, dtos.Count);
        
        Assert.Equal("Honda", dtos[0].Nombre);
        Assert.Equal("Japon", dtos[0].PaisOrigen);

        Assert.Equal("BMW", dtos[1].Nombre);
        Assert.Equal("Alemania", dtos[1].PaisOrigen);
    }

    [Fact]
    public async Task GetAllMarcasAsync_ReturnsEmpty_WhenRepositoryIsEmpty()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<MarcaAuto>());

        // Act
        var result = await _service.GetAllMarcasAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
