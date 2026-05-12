using DLMS.Application.Common.Models;
using MediatR;

namespace DLMS.Application.Features.Items.Commands.UpdateItem;

public record UpdateItemCommand(
    int Id,
    int? TemplateId,
    List<ValueInput> Values
) : IRequest<Unit>;