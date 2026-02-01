using FluentResults;
using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Products.Queries.v1;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;

namespace mrusek.FitTracker.Application.Features.Products.Handlers;

public class GetProductByIdQueryHandler:IQueryHandler<GetProductByIdQuery, GetProductByIdDto>
{
    public Task<Result<GetProductByIdDto>> Handle(GetProductByIdQuery query, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}