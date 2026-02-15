using FluentValidation;
using mrusek.FitTracker.Application.Abstractions.Orchestration;

namespace mrusek.FitTracker.Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse> : ICommandPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        Func<Task<TResponse>> next)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                // Możesz rzucić FluentValidation.ValidationException
                throw new ValidationException(failures);

                // albo zwrócić swój typ Result<TResponse> zamiast wyjątku
            }
        }

        return await next();
    }
}