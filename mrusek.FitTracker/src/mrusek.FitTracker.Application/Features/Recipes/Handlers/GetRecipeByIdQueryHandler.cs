using FluentResults;
using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;
using mrusek.FitTracker.Application.Features.Recipes.Queries.v1;

namespace mrusek.FitTracker.Application.Features.Recipes.Handlers;

public class GetRecipeByIdQueryHandler:IQueryHandler<GetRecipeByIdQuery, GetRecipeByIdDto>
{
    public Task<Result<GetRecipeByIdDto>> Handle(GetRecipeByIdQuery query, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}