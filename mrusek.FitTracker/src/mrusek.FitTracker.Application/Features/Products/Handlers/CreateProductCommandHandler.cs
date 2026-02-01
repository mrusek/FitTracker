using FluentResults;
using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Products.Commands.v1;

namespace mrusek.FitTracker.Application.Features.Products.Handlers;

public class CreateProductCommandHandler:ICommandHandler<CreateProductCommand>
{
    public Task<Result> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}