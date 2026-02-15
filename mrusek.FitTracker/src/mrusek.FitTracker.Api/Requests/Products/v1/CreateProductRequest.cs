using mrusek.FitTracker.Application.Features.Common.Macro.v1;
using mrusek.FitTracker.Application.Features.Products.Dto.v1;

namespace mrusek.FitTracker.Api.Requests.Products.v1;

public sealed record CreateProductRequest(MacroSaveDto Macro,
    string Name,
    string Description,
    IReadOnlyList<string> Tags);
