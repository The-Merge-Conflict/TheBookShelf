using DLMS.Application.Common.Interfaces;
using DLMS.Application.DTOs.Auth;
using MediatR;

namespace DLMS.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler
    : IRequestHandler<GetAllUsersQuery, IReadOnlyList<UserDto>>
{
    private readonly IIdentityService _identityService;

    public GetAllUsersQueryHandler(IIdentityService identityService)
        => _identityService = identityService;

    public Task<IReadOnlyList<UserDto>> Handle(
        GetAllUsersQuery request,
        CancellationToken cancellationToken)
        => _identityService.GetAllUsersAsync();
}
