using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Common.SearchCriteria.v1;
using mrusek.FitTracker.Application.Features.Common.v1;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;

namespace mrusek.FitTracker.Application.Features.Products.Queries.v1;

public sealed record GetProductsBySearchCriteriaQuery():SearchCriteria,IQuery<GetProductsBySearchCriteriaDto>;
