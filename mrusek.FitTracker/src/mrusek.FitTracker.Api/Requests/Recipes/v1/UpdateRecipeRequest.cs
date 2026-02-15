using mrusek.FitTracker.Application.Features.Common.v1;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;

namespace mrusek.FitTracker.Api.Requests.Recipes.v1;

public sealed record UpdateRecipeRequest(
    Guid Id,
    string Name,
    string Description,
    IReadOnlyList<string> Tags,
    IReadOnlyList<Guid> Products,
    MacroUpdateDto Macro);