using MediatR;

namespace DLMS.Application.Features.Properties.Commands.UpdateProperty;

public record UpdatePropertyCommand(
    int Id,
    string LocalName,
    string Label,
    string TermUri
) : IRequest<Unit>;