using FluentValidation;

namespace DLMS.Application.Features.ResourceTemplates.Commands.UpdateTemplateProperty;

public class UpdateTemplatePropertyCommandValidator
    : AbstractValidator<UpdateTemplatePropertyCommand>
{
    public UpdateTemplatePropertyCommandValidator()
    {
        RuleFor(x => x.TemplateId).GreaterThan(0);
        RuleFor(x => x.PropertyId).GreaterThan(0);
        RuleFor(x => x.DisplayOrder).GreaterThanOrEqualTo(0);
        RuleFor(x => x.AlternateLabel).MaximumLength(100);
    }
}
