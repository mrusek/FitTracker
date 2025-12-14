using mrusek.FitTracker.Application.Features.Products.Dto.v1;

namespace mrusek.FitTracker.Api.Requests.Products.v1;

public sealed record CreateProductRequest(
   ProductSaveDto ProductSaveDto);
