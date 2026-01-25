using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;

namespace mrusek.FitTracker.Application.Features.Recipes.Queries.v1;

public sealed record GetRecipesByProductsQuery(ICollection<Guid> Guids) : IQuery<GetRecipesByProductsDto>;