using DLMS.Application.DTOs.Auth;
using MediatR;

namespace DLMS.Application.Features.Auth.Commands.Register;

public record RegisterCommand(
    string UserName,
    string Email,
    string Password,
    string ConfirmPassword
) : IRequest<AuthResponseDto>;