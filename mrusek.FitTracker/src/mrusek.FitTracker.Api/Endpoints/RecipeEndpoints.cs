using Asp.Versioning;
using mrusek.FitTracker.Api.Extensions;
using mrusek.FitTracker.Api.Requests.Recipes.v1;
using mrusek.FitTracker.Application.Abstractions.Mapping;
using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Recipes.Commands.v1;
using mrusek.FitTracker.Application.Features.Recipes.Dto.v1;
using mrusek.FitTracker.Application.Features.Recipes.Queries.v1;

namespace mrusek.FitTracker.Api.Endpoints;

public static class RecipeEndpoints
{
    //Carter??
    public static void MapRecipeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var apiVersionSet = endpoints.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        var group = endpoints.MapGroup("api/v{apiVersion:apiVersion}/recipes")
            .RequireAuthorization("user")
            .WithTags("RecipesV1")
            .WithApiVersionSet(apiVersionSet)
            .RequireRateLimiting("fixed")
            .WithOpenApi();

        group.MapPost("/", CreateRecipe)
            .Accepts<CreateRecipeRequest>("application/json")
            .Produces(201)
            .WithSummary("Creates new recipe");

        group.MapGet("/{id}", GetRecipeById)
            .Produces(200)
            .WithSummary("Get recipe by id");

        group.MapPut("/{id}", UpdateRecipe)
            .Accepts<UpdateRecipeRequest>("application/json")
            .Produces(201)
            .WithSummary("Updates recipe");

        group.MapPost("/search", GetRecipesByProducts)
            .Accepts<GetRecipesByProductsRequest>("application/json")
            .Produces(200)
            .WithSummary("Creates new recipe");

        group.MapGet("/all", GetAllRecipes)
            .Produces(200)
            .WithSummary("Gets all recipes");

        group.MapDelete("/{id}", DeleteRecipe)
            .Produces(204)
            .WithSummary("Deletes recipe");
    }

    private static async Task<IResult> CreateRecipe(CreateRecipeRequest request,
        ICommandHandler<CreateRecipeCommand> broker,
        IRequestToCommandMapper<CreateRecipeRequest, CreateRecipeCommand> mapper,
        CancellationToken cancellationToken)
    {
        var result = await broker.Handle(mapper.Map(request), cancellationToken);
        return result.ToApiResult(Results.Created);
    }

    private static async Task<IResult> GetRecipeById(Guid id,
        IQueryHandler<GetRecipeByIdQuery, GetRecipeByIdDto> broker,
        IRequestToQueryMapper<GetRecipeByIdRequest, GetRecipeByIdQuery, GetRecipeByIdDto> mapper,
        CancellationToken cancellationToken)
    {
        var result = await broker.Handle(mapper.Map(new GetRecipeByIdRequest(id)), cancellationToken);
        return result.ToApiResult();
    }

    private static async Task<IResult> GetAllRecipes(
        IQueryHandler<GetAllRecipesQuery, GetAllRecipesDto> broker,
        IRequestToQueryMapper<GetAllRecipesRequest, GetAllRecipesQuery, GetAllRecipesDto> mapper,
        CancellationToken cancellationToken)
    {
        var result = await broker.Handle(mapper.Map(new GetAllRecipesRequest()), cancellationToken);
        return result.ToApiResult();
    }

    private static async Task<IResult> GetRecipesByProducts(GetRecipesByProductsRequest request,
        IQueryHandler<GetRecipesByProductsQuery, GetRecipesByProductsDto> broker,
        IRequestToQueryMapper<GetRecipesByProductsRequest, GetRecipesByProductsQuery, GetRecipesByProductsDto> mapper,
        CancellationToken cancellationToken)
    {
        var result = await broker.Handle(mapper.Map(request), cancellationToken);
        return result.ToApiResult();
    }

    private static async Task<IResult> DeleteRecipe(Guid id, ICommandHandler<DeleteRecipeCommand> broker,
        IRequestToCommandMapper<DeleteRecipeRequest, DeleteRecipeCommand> mapper,
        CancellationToken cancellationToken)
    {
        var result = await broker.Handle(mapper.Map(new DeleteRecipeRequest(id)), cancellationToken);
        return result.ToApiResult(onSuccess: Results.NoContent);
    }

    private static async Task<IResult> UpdateRecipe(UpdateRecipeRequest request,
        ICommandHandler<UpdateRecipeCommand> broker,
        IRequestToCommandMapper<UpdateRecipeRequest, UpdateRecipeCommand> mapper,
        CancellationToken cancellationToken)
    {
        var result = await broker.Handle(mapper.Map(request), cancellationToken);
        return result.ToApiResult(onSuccess: Results.Created);
    }
}