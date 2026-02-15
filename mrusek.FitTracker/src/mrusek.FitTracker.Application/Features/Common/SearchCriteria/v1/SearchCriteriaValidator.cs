using FluentValidation;
using static mrusek.FitTracker.Application.Common.StringExtensions;
namespace mrusek.FitTracker.Application.Features.Common.SearchCriteria.v1;

public class SearchCriteriaValidator:AbstractValidator<SearchCriteria>
{
    public SearchCriteriaValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(0);
        RuleFor(x=>x.RowsPerPage).GreaterThanOrEqualTo(5);
        RuleFor(x => x.SearchText).MinimumLength(3).Unless(x => x.SearchText.IsNullOrEmpty());
    }
}