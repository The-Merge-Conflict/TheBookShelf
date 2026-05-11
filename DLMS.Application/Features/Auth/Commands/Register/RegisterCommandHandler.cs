using DLMS.Application.Common.Interfaces;
using DLMS.Application.DTOs.Auth;
using MediatR;

namespace DLMS.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IIdentityService _identityService;

    public RegisterCommandHandler(IIdentityService identityService)
        => _identityService = identityService;

    public Task<AuthResponseDto> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
        => _identityService.RegisterAsync(request.UserName, request.Email, request.Password);
}