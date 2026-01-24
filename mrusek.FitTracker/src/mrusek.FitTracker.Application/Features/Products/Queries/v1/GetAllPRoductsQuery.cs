using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Products.Dto.v1;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;

namespace mrusek.FitTracker.Application.Features.Products.Queries.v1;

public sealed record GetAllProductsQuery() : IQuery<GetAllProductsDto>;
