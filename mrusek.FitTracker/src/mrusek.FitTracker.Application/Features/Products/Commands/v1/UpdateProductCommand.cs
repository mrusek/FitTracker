using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Features.Products.Dto.v1;

namespace mrusek.FitTracker.Application.Features.Products.Commands.v1;

public sealed record UpdateProductCommand(ProductSaveDto productSaveDto) : ICommand;