using mrusek.FitTracker.Api.Requests.Products.v1;
using mrusek.FitTracker.Application.Abstractions.Mapping;
using mrusek.FitTracker.Application.Features.Products.Dto.v1;
using mrusek.FitTracker.Application.Features.Products.Queries.v1;
using Riok.Mapperly.Abstractions;

namespace mrusek.FitTracker.Api.Mapping;

#pragma warning disable RMG013
[Mapper]
public partial class GetAllProductsRequestToQueryMapper:IRequestToQueryMapper<GetAllProductsRequest, GetAllProductsQuery, GetAllProductsDto>
{
    public partial GetAllProductsQuery Map(GetAllProductsRequest request);
}