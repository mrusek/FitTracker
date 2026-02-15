using FluentValidation;
using mrusek.FitTracker.Application.Features.Common.Macro.v1;
using mrusek.FitTracker.Application.Features.Products.Commands.v1;

namespace mrusek.FitTracker.Application.Features.Products.Validation.v1;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Macro).SetValidator(new MacroSaveDtoValidator());
    }
}