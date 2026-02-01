using FluentResults;
using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Products.Commands.v1;

namespace mrusek.FitTracker.Application.Features.Products.Handlers;

public class DeleteProductCommandHandler:ICommandHandler<DeleteProductCommand>
{
    public Task<Result> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}