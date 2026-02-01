using mrusek.FitTracker.Application.Features.Common.v1;
using mrusek.FitTracker.Domain.Enums;

namespace mrusek.FitTracker.Application.Features.Products.Dto.v1;

public sealed record ProductOutputDto(
    MacroOutputDto Nutrients, string PortionType, decimal PortionSize, string Name, ICollection<string> Benefits);