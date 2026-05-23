using FluentValidation;

namespace DLMS.Application.Features.Values.Commands.AddValue;

public class AddValueCommandValidator : AbstractValidator<AddValueCommand>
{
    public AddValueCommandValidator()
    {
        RuleFor(x => x.ResourceId).GreaterThan(0);
        RuleFor(x => x.PropertyId).GreaterThan(0);
        RuleFor(x => x.ValueType).IsInEnum();
    }
}
