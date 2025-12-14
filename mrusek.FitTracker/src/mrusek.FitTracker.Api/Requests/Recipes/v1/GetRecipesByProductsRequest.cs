namespace mrusek.FitTracker.Api.Requests.Recipes.v1;

public sealed record GetRecipesByProductsRequest(ICollection<Guid> ProductIds);