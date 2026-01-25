using mrusek.FitTracker.Api.Requests.Recipes.v1;
using mrusek.FitTracker.Application.Abstractions.Mapping;
using mrusek.FitTracker.Application.Features.Recipes.Commands.v1;
using Riok.Mapperly.Abstractions;

namespace mrusek.FitTracker.Api.Mapping;

[Mapper]
public partial class
    UpdateRecipeRequestToCommandMapper : IRequestToCommandMapper<UpdateRecipeRequest, UpdateRecipeCommand>
{
    public partial UpdateRecipeCommand Map(UpdateRecipeRequest request);
}