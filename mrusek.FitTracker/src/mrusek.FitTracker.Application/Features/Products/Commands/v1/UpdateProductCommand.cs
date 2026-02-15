using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Common.v1;
using mrusek.FitTracker.Application.Features.Products.Dto.v1;

namespace mrusek.FitTracker.Application.Features.Products.Commands.v1;

public sealed record UpdateProductCommand( Guid? Id,
    MacroUpdateDto Macro,
    string Name,
    string Description,
    IReadOnlyList<string> Tags) : ICommand;