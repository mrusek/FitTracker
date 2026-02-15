namespace mrusek.FitTracker.Application.Abstractions.Orchestration;

public interface ICommandPipelineBehavior<in TRequest, TResponse>
{
    Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        Func<Task<TResponse>> next);
}