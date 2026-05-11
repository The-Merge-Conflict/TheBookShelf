using FluentValidation;

namespace DLMS.Application.Features.ResourceTemplates.Commands.AddPropertyToTemplate;

public class AddPropertyToTemplateCommandValidator
    : AbstractValidator<AddPropertyToTemplateCommand>
{
    public AddPropertyToTemplateCommandValidator()
    {
        RuleFor(x => x.TemplateId).GreaterThan(0);
        RuleFor(x => x.PropertyId).GreaterThan(0);
        RuleFor(x => x.DisplayOrder).GreaterThanOrEqualTo(0);
        RuleFor(x => x.AlternateLabel).MaximumLength(150);
    }
}