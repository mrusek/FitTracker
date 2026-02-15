using FluentValidation;
using mrusek.FitTracker.Application.Features.Recipes.Queries.v1;

namespace mrusek.FitTracker.Application.Features.Recipes.Validation;

public class GetRecipesByProductsQueryValidator : AbstractValidator<GetRecipesByProductsQuery>
{
    public GetRecipesByProductsQueryValidator()
    {
        RuleFor(x => x.Guids).NotEmpty();
    }
}