using FluentValidation;

namespace DLMS.Application.Features.ItemSets.Commands.DeleteItemSet;

public class DeleteItemSetCommandValidator : AbstractValidator<DeleteItemSetCommand>
{
    public DeleteItemSetCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
