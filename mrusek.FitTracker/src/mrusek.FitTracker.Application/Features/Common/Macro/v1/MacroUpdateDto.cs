namespace mrusek.FitTracker.Application.Features.Common.v1;

public record MacroUpdateDto(Guid Id, decimal Carbs, decimal Fats, decimal Proteins, decimal Calories, decimal Salt, decimal SaturatedFats, decimal SaturatedCarbs);