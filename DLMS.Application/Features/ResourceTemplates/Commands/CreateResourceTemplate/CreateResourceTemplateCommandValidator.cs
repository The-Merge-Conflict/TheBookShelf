using FluentValidation;

namespace DLMS.Application.Features.ResourceTemplates.Commands.CreateResourceTemplate;

public class CreateResourceTemplateCommandValidator
    : AbstractValidator<CreateResourceTemplateCommand>
{
    public CreateResourceTemplateCommandValidator()
    {
        RuleFor(x => x.Label)
            .NotEmpty().WithMessage("Label is required.")
            .MaximumLength(150).WithMessage("Label must not exceed 150 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }
}