using FluentResults;

namespace mrusek.FitTracker.Application.Abstractions.Orchestration;

public interface ICommandHandler<in TCommand> where TCommand: ICommand
{
    Task<Result> Handle(TCommand command, CancellationToken cancellationToken);
}