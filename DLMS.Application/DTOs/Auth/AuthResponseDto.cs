namespace DLMS.Application.DTOs.Auth;

public class AuthResponseDto
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public bool Succeeded { get; set; }
    public IEnumerable<string> Errors { get; set; } = [];
}