using DLMS.Application.Common.Interfaces;
using DLMS.Application.DTOs.Auth;
using MediatR;

namespace DLMS.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IIdentityService _identityService;

    public LoginCommandHandler(IIdentityService identityService)
        => _identityService = identityService;

    public Task<AuthResponseDto> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
        => _identityService.LoginAsync(request.Email, request.Password);
}