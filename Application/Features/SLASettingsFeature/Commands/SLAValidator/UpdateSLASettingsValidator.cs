using Application.Features.SLASettings.Commands;
using FluentValidation;

namespace Application.Features.SLASettings.Validators;

public class UpdateSLASettingValidator : AbstractValidator<UpdateSLASettingCommand>
{
    public UpdateSLASettingValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("SLA Setting Id is required.");

        RuleFor(x => x.Priority)
            .IsInEnum()
            .WithMessage("Invalid Priority.");
    }
}