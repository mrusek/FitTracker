using mrusek.FitTracker.Application.Features.Common.v1;
using mrusek.FitTracker.Application.Features.Products.Dto.v1;

namespace mrusek.FitTracker.Api.Requests.Products.v1;

public sealed record UpdateProductRequest(
    Guid Id,
    MacroUpdateDto Macro,
    string Name,
    string Description,
    IReadOnlyList<string> Tags);