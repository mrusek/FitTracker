using FluentResults;

namespace mrusek.FitTracker.Application.Abstractions.Orchestration;

public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
where TResponse: class
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken token);
}