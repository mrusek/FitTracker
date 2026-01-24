using mrusek.FitTracker.Application.Abstractions.Orchestration;

namespace mrusek.FitTracker.Application.Abstractions.Mapping;

public interface IRequestToQueryMapper<TRequest, TQuery, TResponse>where TRequest : class 
    where TQuery: IQuery<TResponse>
where TResponse: class
{
    public TQuery Map(TRequest request);
}