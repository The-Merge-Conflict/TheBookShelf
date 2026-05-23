using FluentValidation;

namespace DLMS.Application.Features.Values.Commands.UpdateValue;

public class UpdateValueCommandValidator : AbstractValidator<UpdateValueCommand>
{
    public UpdateValueCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.ValueType).IsInEnum();
    }
}
