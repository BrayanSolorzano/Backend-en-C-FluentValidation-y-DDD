using FluentValidation;
using MarcasAutos.Application.DTOs;

namespace MarcasAutos.Application.Validators;

public class MarcaAutoDtoValidator : AbstractValidator<MarcaAutoDto>
{
    public MarcaAutoDtoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre de la marca es requerido.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

        RuleFor(x => x.PaisOrigen)
            .NotEmpty().WithMessage("El país de origen es requerido.")
            .MaximumLength(100).WithMessage("El país de origen no puede exceder los 100 caracteres.");
    }
}
