using MediatR;

namespace DLMS.Application.Features.ItemSets.Commands.DeleteItemSet;

public record DeleteItemSetCommand(int Id) : IRequest<Unit>;