using DLMS.Application.Common.Models;
using MediatR;

namespace DLMS.Application.Features.Items.Commands.CreateItem;

public record CreateItemCommand(
    int? TemplateId,
    List<ValueInput> Values
) : IRequest<int>;