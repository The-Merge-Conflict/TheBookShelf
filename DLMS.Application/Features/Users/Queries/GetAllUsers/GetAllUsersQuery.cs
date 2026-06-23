using DLMS.Application.DTOs.Auth;
using MediatR;

namespace DLMS.Application.Features.Users.Queries.GetAllUsers;

public record GetAllUsersQuery() : IRequest<IReadOnlyList<UserDto>>;
