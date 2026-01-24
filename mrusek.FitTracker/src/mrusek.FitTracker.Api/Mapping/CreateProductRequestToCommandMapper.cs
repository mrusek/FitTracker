using mrusek.FitTracker.Api.Requests.Products.v1;
using mrusek.FitTracker.Application.Abstractions.Mapping;
using mrusek.FitTracker.Application.Features.Products.Commands.v1;
using Riok.Mapperly.Abstractions;

namespace mrusek.FitTracker.Api.Mapping;

[Mapper]
public partial class
    CreateProductRequestToCommandMapper : IRequestToCommandMapper<CreateProductRequest, CreateProductCommand>
{
    public partial CreateProductCommand Map(CreateProductRequest request);
}