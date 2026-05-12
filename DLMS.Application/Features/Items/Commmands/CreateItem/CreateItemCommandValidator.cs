using DLMS.Application.Common.Models;
using FluentValidation;

namespace DLMS.Application.Features.Items.Commands.CreateItem;

public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    private static readonly string[] AllowedTypes = ["literal", "uri", "resource"];

    public CreateItemCommandValidator()
    {
        RuleFor(x => x.Values)
            .NotNull().WithMessage("Values list cannot be null.");

        RuleForEach(x => x.Values).SetValidator(new ValueInputValidator());
    }
}

public class ValueInputValidator : AbstractValidator<ValueInput>
{
    private static readonly Domain.Enums.ValueType[] AllowedTypes = [Domain.Enums.ValueType.Literal, Domain.Enums.ValueType.Uri, Domain.Enums.ValueType.Resource];

    public ValueInputValidator()
    {
        RuleFor(x => x.PropertyId)
            .GreaterThan(0).WithMessage("PropertyId must be a valid positive integer.");

        RuleFor(x => x.Type)
            .Must(t => AllowedTypes.Contains(t))
            .WithMessage($"Type must be one of: {string.Join(", ", AllowedTypes)}.");

        When(x => x.Type == Domain.Enums.ValueType.Literal, () =>
            RuleFor(x => x.ValueText)
                .NotEmpty().WithMessage("ValueText is required for type 'literal'."));

        When(x => x.Type == Domain.Enums.ValueType.Uri, () =>
            RuleFor(x => x.ValueUri)
                .NotEmpty().WithMessage("ValueUri is required for type 'uri'.")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("ValueUri must be a valid absolute URI."));

        When(x => x.Type == Domain.Enums.ValueType.Resource, () =>
            RuleFor(x => x.ValueResourceId)
                .NotNull().GreaterThan(0)
                .WithMessage("ValueResourceId is required for type 'resource'."));
    }
}