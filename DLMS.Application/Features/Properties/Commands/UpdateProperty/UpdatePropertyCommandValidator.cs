using FluentValidation;

namespace DLMS.Application.Features.Properties.Commands.UpdateProperty;

public class UpdatePropertyCommandValidator : AbstractValidator<UpdatePropertyCommand>
{
    public UpdatePropertyCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.LocalName).NotEmpty().MaximumLength(100)
            .Matches("^[a-zA-Z][a-zA-Z0-9_]*$");
        RuleFor(x => x.Label).NotEmpty().MaximumLength(150);
        RuleFor(x => x.TermUri).NotEmpty()
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("TermUri must be a valid absolute URI.");
    }
}