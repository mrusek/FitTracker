using FluentResults;
using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Products.Commands.v1;

namespace mrusek.FitTracker.Application.Features.Products.Handlers;

public class UpdateProductCommandHandler:ICommandHandler<UpdateProductCommand>
{
    public Task<Result> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}