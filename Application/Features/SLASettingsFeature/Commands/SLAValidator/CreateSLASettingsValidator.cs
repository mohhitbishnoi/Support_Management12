using Application.Features.SLASettings.Commands;
using FluentValidation;

namespace Application.Features.SLASettings.Validators;

public class CreateSLASettingValidator : AbstractValidator<CreateSLASettingCommand>
{
    public CreateSLASettingValidator()
    {
        RuleFor(x => x.Priority)
            .IsInEnum()
            .WithMessage("Invalid Priority.");
    }
}