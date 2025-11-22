using mrusek.FitTracker.Application.Abstractions.Orchestration;

namespace mrusek.FitTracker.Api.Requests.Products.v1;

public sealed record DeleteProductRequest(Guid id) : ICommand;
