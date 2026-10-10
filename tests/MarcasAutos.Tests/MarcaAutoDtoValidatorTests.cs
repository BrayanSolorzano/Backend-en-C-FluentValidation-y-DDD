using MarcasAutos.Application.DTOs;
using MarcasAutos.Application.Validators;
using Xunit;

namespace MarcasAutos.Tests.Validators;

public class MarcaAutoDtoValidatorTests
{
    private readonly MarcaAutoDtoValidator _validator;

    public MarcaAutoDtoValidatorTests()
    {
        _validator = new MarcaAutoDtoValidator();
    }

    [Fact]
    public void Validator_ShouldNotHaveError_WhenDtoIsValid()
    {
        var dto = new MarcaAutoDto { Id = 1, Nombre = "Toyota", PaisOrigen = "Japon" };
        var result = _validator.Validate(dto);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validator_ShouldHaveError_WhenNombreIsNullOrEmpty(string? nombre)
    {
        var dto = new MarcaAutoDto { Id = 1, Nombre = nombre!, PaisOrigen = "Japon" };
        var result = _validator.Validate(dto);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Nombre");
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenNombreExceeds100Chars()
    {
        var dto = new MarcaAutoDto { Id = 1, Nombre = new string('A', 101), PaisOrigen = "Japon" };
        var result = _validator.Validate(dto);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Nombre");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validator_ShouldHaveError_WhenPaisOrigenIsNullOrEmpty(string? pais)
    {
        var dto = new MarcaAutoDto { Id = 1, Nombre = "Toyota", PaisOrigen = pais! };
        var result = _validator.Validate(dto);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "PaisOrigen");
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenPaisOrigenExceeds100Chars()
    {
        var dto = new MarcaAutoDto { Id = 1, Nombre = "Toyota", PaisOrigen = new string('B', 101) };
        var result = _validator.Validate(dto);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "PaisOrigen");
    }
}
