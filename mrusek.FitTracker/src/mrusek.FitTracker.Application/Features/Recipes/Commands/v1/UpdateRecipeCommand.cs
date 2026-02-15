using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Common.v1;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;

namespace mrusek.FitTracker.Application.Features.Recipes.Commands.v1;

public sealed record UpdateRecipeCommand(string Name,
    Guid Id,
    string Description,
    IReadOnlyList<string> Tags,
    IReadOnlyList<Guid> Products,
    MacroUpdateDto Macro) : ICommand;