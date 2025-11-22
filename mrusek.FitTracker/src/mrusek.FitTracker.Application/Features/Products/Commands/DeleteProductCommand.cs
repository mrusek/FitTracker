using ICommand = mrusek.FitTracker.Application.Abstractions.Orchestration.ICommand;

namespace mrusek.FitTracker.Application.Features.Products.Commands;

public sealed record DeleteProductCommand(Guid ProductId): ICommand;
