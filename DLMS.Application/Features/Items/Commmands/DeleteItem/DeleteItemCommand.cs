using MediatR;

namespace DLMS.Application.Features.Items.Commands.DeleteItem;

public record DeleteItemCommand(int Id) : IRequest<Unit>;