using FluentValidation;

namespace DLMS.Application.Features.Properties.Commands.CreateProperty;

public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyCommandValidator()
    {
        RuleFor(x => x.VocabularyId)
            .GreaterThan(0).WithMessage("A valid VocabularyId is required.");

        RuleFor(x => x.LocalName)
            .NotEmpty().WithMessage("LocalName is required.")
            .MaximumLength(100)
            .Matches("^[a-zA-Z][a-zA-Z0-9_]*$")
            .WithMessage("LocalName must start with a letter and contain only letters, digits, or underscores.");

        RuleFor(x => x.Label)
            .NotEmpty().WithMessage("Label is required.")
            .MaximumLength(150);

        RuleFor(x => x.TermUri)
            .NotEmpty().WithMessage("TermUri is required.")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("TermUri must be a valid absolute URI.");
    }
}