using mrusek.FitTracker.Api.Requests.Recipes.v1;
using mrusek.FitTracker.Application.Abstractions.Mapping;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;
using mrusek.FitTracker.Application.Features.Recipes.Queries.v1;
using Riok.Mapperly.Abstractions;

namespace mrusek.FitTracker.Api.Mapping;

#pragma warning disable RMG013
[Mapper]
public partial class
    GetAllRecipesRequestToQueryMapper : IRequestToQueryMapper<GetAllRecipesRequest, GetAllRecipesQuery,
    GetAllRecipesDto>
{
    public partial GetAllRecipesQuery Map(GetAllRecipesRequest request);
}