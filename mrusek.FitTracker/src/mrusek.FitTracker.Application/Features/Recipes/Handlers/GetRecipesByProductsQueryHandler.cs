using FluentResults;
using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;
using mrusek.FitTracker.Application.Features.Recipes.Queries.v1;

namespace mrusek.FitTracker.Application.Features.Recipes.Handlers;

public class GetRecipesByProductsQueryHandler:IQueryHandler<GetRecipesByProductsQuery, GetRecipesByProductsDto>
{
    public Task<Result<GetRecipesByProductsDto>> Handle(GetRecipesByProductsQuery query, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}