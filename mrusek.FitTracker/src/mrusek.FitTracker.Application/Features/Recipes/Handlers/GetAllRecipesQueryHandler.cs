using FluentResults;
using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;
using mrusek.FitTracker.Application.Features.Recipes.Queries.v1;

namespace mrusek.FitTracker.Application.Features.Recipes.Handlers;

public class GetAllRecipesQueryHandler:IQueryHandler<GetAllRecipesQuery, GetAllRecipesDto>
{
    public Task<Result<GetAllRecipesDto>> Handle(GetAllRecipesQuery query, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}