using FluentValidation;
using mrusek.FitTracker.Application.Features.Common.Macro.v1;
using mrusek.FitTracker.Application.Features.Recipes.Commands.v1;

namespace mrusek.FitTracker.Application.Features.Recipes.Validation;

public class CreateRecipeCommandValidator : AbstractValidator<CreateRecipeCommand>
{
    public CreateRecipeCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.Macro).SetValidator(new MacroSaveDtoValidator());
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Products).NotEmpty();
    }
}