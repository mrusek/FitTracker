using mrusek.FitTracker.Application.Abstractions.Orchestration;

namespace mrusek.FitTracker.Application.Abstractions.Mapping;

public interface IRequestToCommandMapper<TRequest, TCommand>
    where TRequest : class
    where TCommand : ICommand
{
    public TCommand Map(TRequest request);
}