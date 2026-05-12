using FluentValidation;

namespace DLMS.Application.Features.ItemSets.Commands.AddItemToSet;

public class AddItemToSetCommandValidator : AbstractValidator<AddItemToSetCommand>
{
    public AddItemToSetCommandValidator()
    {
        RuleFor(x => x.ItemSetId).GreaterThan(0);
        RuleFor(x => x.ItemId).GreaterThan(0);
    }
}