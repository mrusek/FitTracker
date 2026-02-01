using FluentResults;
using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Recipes.Commands.v1;

namespace mrusek.FitTracker.Application.Features.Recipes.Handlers;

public class UpdateRecipeCommandHandler:ICommandHandler<UpdateRecipeCommand>
{
    public Task<Result> Handle(UpdateRecipeCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}