using MediatR;

namespace DLMS.Application.Features.Values.Commands.DeleteValue;

public record DeleteValueCommand(int Id) : IRequest<Unit>;
