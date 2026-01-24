using Asp.Versioning;
using mrusek.FitTracker.Api.Extensions;
using mrusek.FitTracker.Api.Requests.Products.v1;
using mrusek.FitTracker.Application.Abstractions.Mapping;
using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Products.Commands.v1;
using mrusek.FitTracker.Application.Features.Products.Dto.v1;
using mrusek.FitTracker.Application.Features.Products.Queries;
using mrusek.FitTracker.Application.Features.Products.Queries.v1;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;

namespace mrusek.FitTracker.Api.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var apiVersionSet = endpoints.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        var group = endpoints.MapGroup("api/v{apiVersion:apiVersion}/products")
            .RequireAuthorization("user")
            .WithTags("ProductsV1")
            .WithOpenApi()
            .WithApiVersionSet(apiVersionSet)
            .RequireRateLimiting("fixed");

        group.MapPost("/", CreateProduct)
            .Accepts<CreateProductRequest>("application/json")
            .Produces(201)
            .WithSummary("Creates new product");

        group.MapGet("/{id}", GetProductById)
            .Produces(200)
            .WithSummary("Gets product by Id");

        group.MapPost("/{id}", UpdateProduct)
            .Accepts<UpdateProductRequest>("application/json")
            .Produces(201)
            .WithSummary("Updates product");

        group.MapDelete("/{id}", DeleteProduct)
            .Produces(204)
            .WithSummary(" product")
            .WithSummary("Deletes product");

        group.MapGet("/", GetAllProducts)
            .Produces(200)
            .WithSummary("Gets all products");

        group.MapPost("/search", GetProductsBySearchCriteria)
            .Accepts<GetProductsBySearchCriteriaRequest>("application/json")
            .Produces(201)
            .WithSummary("Gets products by search criteria");
    }

    private static async Task<IResult> CreateProduct(CreateProductRequest request,
        ICommandHandler<CreateProductCommand> broker,
        IRequestToCommandMapper<CreateProductRequest, CreateProductCommand> mapper,
        CancellationToken cancellationToken)
    {
        var result = await broker.Handle(mapper.Map(request), cancellationToken);
        return result.ToApiResult(Results.Created);
    }

    private static async Task<IResult> GetProductById(Guid id,
        IQueryHandler<GetProductByIdQuery, GetProductByIdDto> broker,
        IRequestToQueryMapper<GetProductByIdRequest, GetProductByIdQuery, GetProductByIdDto> mapper,
        CancellationToken cancellationToken)
    {
        var result = await broker.Handle(mapper.Map(new GetProductByIdRequest(id)), cancellationToken);
        return result.ToApiResult();
    }

    private static async Task<IResult> GetAllProducts(
        IQueryHandler<GetAllProductsQuery, GetAllProductsDto> broker,
        IRequestToQueryMapper<GetAllProductsRequest, GetAllProductsQuery, GetAllProductsDto> mapper,
        CancellationToken cancellationToken)
    {
        var result = await broker.Handle(mapper.Map(new GetAllProductsRequest()), cancellationToken);
        return result.ToApiResult();
    }

    private static async Task<IResult> GetProductsBySearchCriteria(GetProductsBySearchCriteriaRequest request,
        IQueryHandler<GetProductsBySearchCriteriaQuery, GetProductsBySearchCriteriaDto> broker,
        IRequestToQueryMapper<GetProductsBySearchCriteriaRequest, GetProductsBySearchCriteriaQuery, GetProductsBySearchCriteriaDto> mapper,
        CancellationToken cancellationToken)
    {
        var result = await broker.Handle(mapper.Map(request), cancellationToken);
        return result.ToApiResult();
    }

    private static async Task<IResult> DeleteProduct(Guid id, ICommandHandler<DeleteProductCommand> broker,
        IRequestToCommandMapper<DeleteProductRequest, DeleteProductCommand> mapper,
        CancellationToken cancellationToken)
    {
        var result = await broker.Handle(mapper.Map(new DeleteProductRequest(id)), cancellationToken);
        return result.ToApiResult(onSuccess: Results.NoContent);
    }

    private static async Task<IResult> UpdateProduct(UpdateProductRequest request,
        ICommandHandler<UpdateProductCommand> broker,
        IRequestToCommandMapper<UpdateProductRequest, UpdateProductCommand> mapper,
        CancellationToken cancellationToken)
    {
        var result = await broker.Handle(mapper.Map(request), cancellationToken);
        return result.ToApiResult(onSuccess: Results.Created);
    }
}