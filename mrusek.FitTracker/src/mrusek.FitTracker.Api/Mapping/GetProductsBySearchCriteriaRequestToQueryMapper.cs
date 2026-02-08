using mrusek.FitTracker.Api.Requests.Products.v1;
using mrusek.FitTracker.Application.Abstractions.Mapping;
using mrusek.FitTracker.Application.Features.Products.Queries.v1;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;
using Riok.Mapperly.Abstractions;

namespace mrusek.FitTracker.Api.Mapping;

#pragma warning disable RMG013
[Mapper]
public partial class GetProductsBySearchCriteriaRequestToQueryMapper : IRequestToQueryMapper<
    GetProductsBySearchCriteriaRequest, GetProductsBySearchCriteriaQuery, GetProductsBySearchCriteriaDto>
{
    public partial GetProductsBySearchCriteriaQuery Map(GetProductsBySearchCriteriaRequest request);
}