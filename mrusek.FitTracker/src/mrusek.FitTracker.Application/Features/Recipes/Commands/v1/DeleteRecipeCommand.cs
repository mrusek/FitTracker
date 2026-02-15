using mrusek.FitTracker.Application.Abstractions.Orchestration;

namespace mrusek.FitTracker.Application.Features.Recipes.Commands.v1;

public sealed record DeleteRecipeCommand(Guid Id) : ICommand;
