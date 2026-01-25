using FluentValidation;
using mrusek.FitTracker.Application.Features.Products.Queries;
using mrusek.FitTracker.Application.Features.Products.Queries.v1;

namespace mrusek.FitTracker.Application.Features.Products.Validation.v1;

public class GetProductByIdQueryValidator:AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}