using FluentValidation;

namespace DLMS.Application.Features.Vocabularies.Commands.DeleteVocabulary;

public class DeleteVocabularyCommandValidator : AbstractValidator<DeleteVocabularyCommand>
{
    public DeleteVocabularyCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
