namespace DLMS.Application.DTOs.Auth;

public class AuthResponseDto
{
    public bool Succeeded { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string[] Roles { get; set; } = [];
    public string[] Errors { get; set; } = [];
}