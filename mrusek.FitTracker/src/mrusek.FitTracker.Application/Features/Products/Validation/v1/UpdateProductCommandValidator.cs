using FluentValidation;
using mrusek.FitTracker.Application.Features.Common.Macro.v1;
using mrusek.FitTracker.Application.Features.Products.Commands;
using mrusek.FitTracker.Application.Features.Products.Commands.v1;

namespace mrusek.FitTracker.Application.Features.Products.Validation.v1;

public class UpdateProductCommandValidator:AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Macro).SetValidator(new MacroUpdateDtoValidator());
    }
}