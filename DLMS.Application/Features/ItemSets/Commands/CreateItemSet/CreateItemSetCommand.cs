using MediatR;

namespace DLMS.Application.Features.ItemSets.Commands.CreateItemSet;

public record CreateItemSetCommand(
    string Title,
    string Description,
    bool IsPublic
) : IRequest<int>;