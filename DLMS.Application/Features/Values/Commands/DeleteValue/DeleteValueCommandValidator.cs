using FluentValidation;

namespace DLMS.Application.Features.Values.Commands.DeleteValue;

public class DeleteValueCommandValidator : AbstractValidator<DeleteValueCommand>
{
    public DeleteValueCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
