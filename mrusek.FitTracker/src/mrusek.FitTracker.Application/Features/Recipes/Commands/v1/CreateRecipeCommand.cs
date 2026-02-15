using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Common.Macro.v1;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;

namespace mrusek.FitTracker.Application.Features.Recipes.Commands.v1;

public sealed record CreateRecipeCommand(string Name,
    string Description,
    IReadOnlyList<string> Tags,
    IReadOnlyList<Guid> Products,
    MacroSaveDto Macro) : ICommand;