using mrusek.FitTracker.Application.Features.Common.SearchCriteria.v1;

namespace mrusek.FitTracker.Api.Requests.Products.v1;

public sealed record GetProductsBySearchCriteriaRequest(SearchCriteria SearchCriteria);