using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Common.v1;

namespace mrusek.FitTracker.Api.Requests.Recipes.v1;

public class CreateRecipeRequest(
    string Name,
    string Description,
    IReadOnlyList<string> Tags,
    IReadOnlyList<Guid> products,
    MacroSaveDto macro) : ICommand
{
}