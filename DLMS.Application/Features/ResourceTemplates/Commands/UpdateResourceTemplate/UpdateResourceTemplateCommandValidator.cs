using FluentValidation;

namespace DLMS.Application.Features.ResourceTemplates.Commands.UpdateResourceTemplate;

public class UpdateResourceTemplateCommandValidator
    : AbstractValidator<UpdateResourceTemplateCommand>
{
    public UpdateResourceTemplateCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Label).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Description).MaximumLength(500);
    }
}