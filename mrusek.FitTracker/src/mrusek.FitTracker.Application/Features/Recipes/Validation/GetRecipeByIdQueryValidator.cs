using FluentValidation;
using mrusek.FitTracker.Application.Features.Recipes.Queries.v1;

namespace mrusek.FitTracker.Application.Features.Recipes.Validation;

public class GetRecipeByIdQueryValidator : AbstractValidator<GetRecipeByIdQuery>
{
    public GetRecipeByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}