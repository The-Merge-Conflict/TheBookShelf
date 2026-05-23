using FluentValidation;

namespace DLMS.Application.Features.Properties.Commands.DeleteProperty;

public class DeletePropertyCommandValidator : AbstractValidator<DeletePropertyCommand>
{
    public DeletePropertyCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
