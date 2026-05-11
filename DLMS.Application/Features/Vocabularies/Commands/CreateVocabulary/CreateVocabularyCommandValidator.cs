using FluentValidation;

namespace DLMS.Application.Features.Vocabularies.Commands.CreateVocabulary;

public class CreateVocabularyCommandValidator : AbstractValidator<CreateVocabularyCommand>
{
    public CreateVocabularyCommandValidator()
    {
        RuleFor(x => x.Prefix)
            .NotEmpty().WithMessage("Prefix is required.")
            .MaximumLength(20).WithMessage("Prefix must not exceed 20 characters.")
            .Matches("^[a-z]+$").WithMessage("Prefix must be lowercase letters only (e.g., dc, foaf).");

        RuleFor(x => x.NamespaceUri)
            .NotEmpty().WithMessage("Namespace URI is required.")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Namespace URI must be a valid absolute URI.");

        RuleFor(x => x.Label)
            .NotEmpty().WithMessage("Label is required.")
            .MaximumLength(100).WithMessage("Label must not exceed 100 characters.");
    }
}