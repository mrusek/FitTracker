using mrusek.FitTracker.Api.Requests.Products.v1;
using mrusek.FitTracker.Application.Abstractions.Mapping;
using mrusek.FitTracker.Application.Features.Products.Queries.v1;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;
using Riok.Mapperly.Abstractions;

namespace mrusek.FitTracker.Api.Mapping;

[Mapper]
public partial class GetProductByIdRequestToQueryMapper:IRequestToQueryMapper<GetProductByIdRequest, GetProductByIdQuery, GetProductByIdDto>
{
    public partial GetProductByIdQuery Map(GetProductByIdRequest request);
}