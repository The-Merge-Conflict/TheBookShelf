using DLMS.Application.DTOs.Auth;

namespace DLMS.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<AuthResponseDto> RegisterAsync(string userName, string email, string password);
    Task<AuthResponseDto> LoginAsync(string email, string password);
    Task<string?> GetUserNameAsync(string userId);
    Task<bool> IsInRoleAsync(string userId, string role);
}