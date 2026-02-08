using mrusek.FitTracker.Api.Requests.Recipes.v1;
using mrusek.FitTracker.Application.Abstractions.Mapping;
using mrusek.FitTracker.Application.Features.Recipes.Commands.v1;
using Riok.Mapperly.Abstractions;

namespace mrusek.FitTracker.Api.Mapping;

#pragma warning disable RMG013
[Mapper]
public partial class
    CreateRecipeRequestToCommandMapper : IRequestToCommandMapper<CreateRecipeRequest, CreateRecipeCommand>
{
    public partial CreateRecipeCommand Map(CreateRecipeRequest request);
}