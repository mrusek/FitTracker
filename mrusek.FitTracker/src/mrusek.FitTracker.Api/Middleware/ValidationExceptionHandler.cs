using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace mrusek.FitTracker.Api.Middleware;

public class ValidationExceptionHandler(IProblemDetailsService problemDetailsService, ILogger logger): IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is ValidationException validationException)
        {
            logger.LogError(exception,
                "Błąd walidacji dla żądania {RequestPath}. TraceId: {TraceId}",
                httpContext.Request.Path, httpContext.TraceIdentifier);

            var errors = validationException.Errors
                .Select(e => new { e.PropertyName, e.ErrorMessage });
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status422UnprocessableEntity,
                Title = "Validation failed",
                Type = validationException.GetType().Name,
                Detail = JsonConvert.SerializeObject(errors, Formatting.Indented)
            };

            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                Exception = validationException,
                HttpContext = httpContext,
                ProblemDetails = problemDetails
            });
        }
        return false;
    }
}