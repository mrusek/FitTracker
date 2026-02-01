namespace mrusek.FitTracker.Application.Features.Products.Dto.v1;

public sealed record GetAllProductsDto(ICollection<ProductOutputDto> Products);