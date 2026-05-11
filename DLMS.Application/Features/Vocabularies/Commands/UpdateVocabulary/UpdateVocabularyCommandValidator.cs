using FluentValidation;

namespace DLMS.Application.Features.Vocabularies.Commands.UpdateVocabulary;

public class UpdateVocabularyCommandValidator : AbstractValidator<UpdateVocabularyCommand>
{
    public UpdateVocabularyCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("A valid Vocabulary Id is required.");

        RuleFor(x => x.Prefix)
            .NotEmpty().WithMessage("Prefix is required.")
            .MaximumLength(20)
            .Matches("^[a-z]+$").WithMessage("Prefix must be lowercase letters only.");

        RuleFor(x => x.NamespaceUri)
            .NotEmpty()
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Namespace URI must be a valid absolute URI.");

        RuleFor(x => x.Label)
            .NotEmpty().MaximumLength(100);
    }
}