using mrusek.FitTracker.Application.Abstractions.Orchestration;

namespace mrusek.FitTracker.Api.Requests.Products.v1;

public sealed record CreateProductRequest(
    string Name,
    string Description,
    IReadOnlyList<string> Tags) : ICommand;
