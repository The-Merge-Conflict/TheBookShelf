using FluentValidation;

namespace DLMS.Application.Features.ResourceTemplates.Commands.RemovePropertyFromTemplate;

public class RemovePropertyFromTemplateCommandValidator : AbstractValidator<RemovePropertyFromTemplateCommand>
{
    public RemovePropertyFromTemplateCommandValidator()
    {
        RuleFor(x => x.TemplateId).GreaterThan(0);
        RuleFor(x => x.PropertyId).GreaterThan(0);
    }
}
