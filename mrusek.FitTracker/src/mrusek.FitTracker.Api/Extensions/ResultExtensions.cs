using FluentResults;
using Microsoft.AspNetCore.Mvc;
using static System.ArgumentNullException;

namespace mrusek.FitTracker.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToApiResult<T>(this Result<T> result, Func<IResult> onSuccess)
    {
        ThrowIfNull(onSuccess);
        if (result.IsSuccess)
            return onSuccess();

        var firstError = result.Errors.FirstOrDefault();

        var statusCode = MapStatusCode(firstError);
        var problem = new ProblemDetails
        {
            Title = "Operation failed",
            Detail = firstError?.Message,
            Status = statusCode,
            Type = $"https://httpstatuses.com/{statusCode}",
        };

        // Dodajemy metadane z FluentResults (np. ErrorCode, TraceId, itp.)
        foreach (var kv in firstError?.Metadata ?? [])
        {
            problem.Extensions[kv.Key] = kv.Value;
        }

        return Results.Problem(problem);
    }

    public static IResult ToApiResult(this Result result, Func<IResult> onSuccess)
    {
        ThrowIfNull(onSuccess);
        if (result.IsSuccess)
            return onSuccess();

        var firstError = result.Errors.FirstOrDefault();

        var statusCode = MapStatusCode(firstError);
        var problem = new ProblemDetails
        {
            Title = "Operation failed",
            Detail = firstError?.Message,
            Status = statusCode,
            Type = $"https://httpstatuses.com/{statusCode}",
        };

        // Dodajemy metadane z FluentResults (np. ErrorCode, TraceId, itp.)
        foreach (var kv in firstError?.Metadata ?? [])
        {
            problem.Extensions[kv.Key] = kv.Value;
        }

        return Results.Problem(problem);
    }

    public static IResult ToApiResult(this Result result)
    {
        return ToApiResult(result, () => Results.Ok());
    }

    public static IResult ToApiResult<T>(this Result<T> result)
    {
        return ToApiResult(result, () => Results.Ok(result.Value));
    }

    private static int MapStatusCode(IError? error)
    {
        if (error?.Metadata.TryGetValue("ErrorCode", out var codeObj) == true)
        {
            var code = codeObj?.ToString();
            return code switch
            {
                "NotFound" => StatusCodes.Status404NotFound,
                "Conflict" => StatusCodes.Status409Conflict,
                "Unauthorized" => StatusCodes.Status401Unauthorized,
                "Forbidden" => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status400BadRequest
            };
        }

        return StatusCodes.Status400BadRequest;
    }
}