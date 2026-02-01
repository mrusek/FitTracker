using FluentResults;
using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Products.Queries.v1;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;

namespace mrusek.FitTracker.Application.Features.Products.Handlers;

public class GetProductsBySearchCriteriaQueryHandler:IQueryHandler<GetProductsBySearchCriteriaQuery, GetProductsBySearchCriteriaDto>
{
    public Task<Result<GetProductsBySearchCriteriaDto>> Handle(GetProductsBySearchCriteriaQuery query, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}