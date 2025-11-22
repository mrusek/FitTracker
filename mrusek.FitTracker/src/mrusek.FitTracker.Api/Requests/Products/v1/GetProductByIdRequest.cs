using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;

namespace mrusek.FitTracker.Api.Requests.Products.v1;

public sealed record GetProductByIdRequest(Guid id): IQuery<GetProductByIdDto>;