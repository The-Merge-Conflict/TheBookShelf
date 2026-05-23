using FluentValidation;

namespace DLMS.Application.Features.ResourceTemplates.Commands.DeleteResourceTemplate;

public class DeleteResourceTemplateCommandValidator : AbstractValidator<DeleteResourceTemplateCommand>
{
    public DeleteResourceTemplateCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
