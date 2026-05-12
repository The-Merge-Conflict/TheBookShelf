using MediatR;

namespace DLMS.Application.Features.ItemSets.Commands.UpdateItemSet;

public record UpdateItemSetCommand(
    int Id,
    string Title,
    string Description,
    bool IsPublic
) : IRequest<Unit>;