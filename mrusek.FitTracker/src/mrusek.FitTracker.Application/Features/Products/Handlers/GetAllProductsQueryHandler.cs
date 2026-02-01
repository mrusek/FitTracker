using FluentResults;
using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Products.Dto.v1;
using mrusek.FitTracker.Application.Features.Products.Queries.v1;

namespace mrusek.FitTracker.Application.Features.Products.Handlers;

public class GetAllProductsQueryHandler:IQueryHandler<GetAllProductsQuery, GetAllProductsDto>
{
    public Task<Result<GetAllProductsDto>> Handle(GetAllProductsQuery query, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}