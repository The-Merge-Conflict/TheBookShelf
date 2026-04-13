namespace TBS.Application.Features.Identity.Commands.LoginUser;

public record AuthTokenDto(
    string AccessToken,
    DateTime ExpiresAt,
    string Email,
    IEnumerable<string> Roles
);