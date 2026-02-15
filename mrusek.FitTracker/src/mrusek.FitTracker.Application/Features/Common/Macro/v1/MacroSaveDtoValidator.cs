using FluentValidation;

namespace mrusek.FitTracker.Application.Features.Common.Macro.v1;

public class MacroSaveDtoValidator:AbstractValidator<MacroSaveDto>
{
    public MacroSaveDtoValidator()
    {
        RuleFor(x=>x.Calories).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(x => x.SaturatedCarbs).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(x => x.Carbs).NotEmpty().GreaterThanOrEqualTo(x=>x.SaturatedCarbs);
        RuleFor(x => x.SaturatedFats).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(x => x.Fats).NotEmpty().GreaterThanOrEqualTo(x=>x.SaturatedFats);
        RuleFor(x => x.Proteins).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(x=>x.Salt).NotEmpty().GreaterThanOrEqualTo(0);
    }
}