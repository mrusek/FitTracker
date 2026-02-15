using FluentValidation;
using mrusek.FitTracker.Application.Features.Recipes.Commands.v1;

namespace mrusek.FitTracker.Application.Features.Recipes.Validation;

public class DeleteRecipeCommandValidator:AbstractValidator<DeleteRecipeCommand>
{
    public DeleteRecipeCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}