using FluentResults;
using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Recipes.Commands.v1;

namespace mrusek.FitTracker.Application.Features.Recipes.Handlers;

public class CreateRecipeCommandHandler : ICommandHandler<CreateRecipeCommand>
{
    public Task<Result> Handle(CreateRecipeCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}