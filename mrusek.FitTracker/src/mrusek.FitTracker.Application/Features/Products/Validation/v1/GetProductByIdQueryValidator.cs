using FluentValidation;
using mrusek.FitTracker.Application.Features.Products.Queries;

namespace mrusek.FitTracker.Application.Features.Products.Validation.v1;

public class GetProductByIdQueryValidator:AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}