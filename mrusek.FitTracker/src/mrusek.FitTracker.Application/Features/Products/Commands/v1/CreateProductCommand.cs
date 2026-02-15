using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Common.Macro.v1;
using mrusek.FitTracker.Application.Features.Common.v1;
using mrusek.FitTracker.Application.Features.Products.Dto.v1;

namespace mrusek.FitTracker.Application.Features.Products.Commands.v1;

public sealed record CreateProductCommand(
    MacroSaveDto Macro,
    string Name,
    string Description,
    IReadOnlyList<string> Tags) : ICommand;
