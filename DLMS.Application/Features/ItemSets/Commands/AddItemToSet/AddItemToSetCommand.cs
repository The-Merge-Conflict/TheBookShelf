using MediatR;

namespace DLMS.Application.Features.ItemSets.Commands.AddItemToSet;

public record AddItemToSetCommand(int ItemSetId, int ItemId) : IRequest<Unit>;