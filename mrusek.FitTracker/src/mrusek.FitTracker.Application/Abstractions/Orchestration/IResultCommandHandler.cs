using FluentResults;

namespace mrusek.FitTracker.Application.Abstractions.Orchestration;

public interface IResultCommandHandler<in TCommand, TResponse> where TCommand : IResultCommand<TResponse>
{
    Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
}