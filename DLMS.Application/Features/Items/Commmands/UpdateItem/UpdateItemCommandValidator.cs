using DLMS.Application.Features.Items.Commands.CreateItem;
using FluentValidation;

namespace DLMS.Application.Features.Items.Commands.UpdateItem;

public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Values).NotNull();
        RuleForEach(x => x.Values).SetValidator(new ValueInputValidator());
    }
}