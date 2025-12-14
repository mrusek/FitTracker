using mrusek.FitTracker.Application.Features.Common.v1;

namespace mrusek.FitTracker.Application.Features.Recipes.Dto.v1;

public sealed record RecipeSaveDto( string Name,
    Guid? Id,
    string Description,
    IReadOnlyList<string> Tags,
    IReadOnlyList<Guid> Products,
    MacroSaveDto Macro);