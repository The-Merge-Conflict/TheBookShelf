namespace TBS.Application.Features.Identity.Queries.GetCurrentUser;

public record UserProfileDto(
    string Id,
    string Email,
    string DisplayName,
    IEnumerable<string> Roles
);