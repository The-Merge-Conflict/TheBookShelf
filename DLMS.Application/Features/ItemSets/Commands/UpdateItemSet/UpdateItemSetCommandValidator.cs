using FluentValidation;

namespace DLMS.Application.Features.ItemSets.Commands.UpdateItemSet;

public class UpdateItemSetCommandValidator : AbstractValidator<UpdateItemSetCommand>
{
    public UpdateItemSetCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).MaximumLength(1000);
    }
}