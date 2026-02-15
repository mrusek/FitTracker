using mrusek.FitTracker.Application.Features.Common.Macro.v1;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;

namespace mrusek.FitTracker.Api.Requests.Recipes.v1;

public sealed record CreateRecipeRequest(string Name,
    string Description,
    IReadOnlyList<string> Tags,
    IReadOnlyList<Guid> Products,
    MacroSaveDto Macro);