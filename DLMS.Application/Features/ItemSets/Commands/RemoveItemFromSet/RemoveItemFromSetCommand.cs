using MediatR;

namespace DLMS.Application.Features.ItemSets.Commands.RemoveItemFromSet;

public record RemoveItemFromSetCommand(int ItemSetId, int ItemId) : IRequest<Unit>;