using FluentValidation;
using mrusek.FitTracker.Application.Features.Common.SearchCriteria.v1;
using mrusek.FitTracker.Application.Features.Products.Queries.v1;

namespace mrusek.FitTracker.Application.Features.Products.Validation.v1;

public class GetProductsBySearchCriteriaQueryValidator : AbstractValidator<GetProductsBySearchCriteriaQuery>
{
    public GetProductsBySearchCriteriaQueryValidator()
    {
        RuleFor(x => x).SetValidator(new SearchCriteriaValidator());
    }
}