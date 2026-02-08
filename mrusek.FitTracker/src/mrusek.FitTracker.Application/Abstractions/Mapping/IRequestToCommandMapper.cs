using mrusek.FitTracker.Application.Abstractions.Orchestration;

namespace mrusek.FitTracker.Application.Abstractions.Mapping;

public interface IRequestToCommandMapper<in TRequest, out TCommand>
    where TRequest : class
    where TCommand : ICommand
{
    public TCommand Map(TRequest request);
}