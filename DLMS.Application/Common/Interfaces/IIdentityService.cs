using DLMS.Application.DTOs.Auth;

namespace DLMS.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<AuthResponseDto> RegisterAsync(string userName, string email, string password, string role);
    Task<AuthResponseDto> LoginAsync(string email, string password);
}