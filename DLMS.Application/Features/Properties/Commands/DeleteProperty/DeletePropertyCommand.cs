using MediatR;

namespace DLMS.Application.Features.Properties.Commands.DeleteProperty;

public record DeletePropertyCommand(int Id) : IRequest<Unit>;