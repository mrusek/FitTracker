using mrusek.FitTracker.Application.Features.Common.v1;

namespace mrusek.FitTracker.Application.Features.Products.Dto.v1;

public sealed record ProductSaveDto(
    Guid? Id,
    MacroSaveDto Macro,
    string Name,
    string Description,
    IReadOnlyList<string> Tags);
