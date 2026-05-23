using FluentValidation;

namespace DLMS.Application.Features.ItemSets.Commands.RemoveItemFromSet;

public class RemoveItemFromSetCommandValidator : AbstractValidator<RemoveItemFromSetCommand>
{
    public RemoveItemFromSetCommandValidator()
    {
        RuleFor(x => x.ItemSetId).GreaterThan(0);
        RuleFor(x => x.ItemId).GreaterThan(0);
    }
}
