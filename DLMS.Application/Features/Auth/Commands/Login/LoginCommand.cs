using DLMS.Application.DTOs.Auth;
using MediatR;

namespace DLMS.Application.Features.Auth.Commands.Login;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<AuthResponseDto>;