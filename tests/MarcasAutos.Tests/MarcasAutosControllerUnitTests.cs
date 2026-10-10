using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarcasAutos.API.Controllers;
using MarcasAutos.Application.DTOs;
using MarcasAutos.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace MarcasAutos.Tests.Controllers;

public class MarcasAutosControllerUnitTests
{
    private readonly Mock<IMarcaAutoService> _mockService;
    private readonly MarcasAutosController _controller;

    public MarcasAutosControllerUnitTests()
    {
        _mockService = new Mock<IMarcaAutoService>();
        _controller = new MarcasAutosController(_mockService.Object);
    }

    [Fact]
    public async Task Get_ReturnsOkResult_WithData()
    {
        // Arrange
        var mockData = new List<MarcaAutoDto>
        {
            new MarcaAutoDto { Id = 1, Nombre = "Nissan", PaisOrigen = "Japon" }
        };
        _mockService.Setup(s => s.GetAllMarcasAsync()).ReturnsAsync(mockData);

        // Act
        var result = await _controller.Get();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<MarcaAutoDto>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<MarcaAutoDto>>(okResult.Value);
        Assert.Single(returnValue);
        Assert.Equal("Nissan", returnValue.First().Nombre);
    }
}
