using MediatR;

namespace DLMS.Application.Features.Media.Commands.DeleteMedia;

public record DeleteMediaCommand(int Id) : IRequest<Unit>;
