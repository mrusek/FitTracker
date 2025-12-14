namespace mrusek.FitTracker.Application.Features.Common.SearchCriteria.v1;

public record SearchCriteria
{
    public int PageNumber { get; init; } = 0;
    public int RowsPerPage { get; init; } = 20;
    public string? SearchText { get; init; } = string.Empty;
}