using DLMS.Application.Common.Interfaces;
using DLMS.Application.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        JwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto> RegisterAsync(
        string userName, string email, string password, string role)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            return new AuthResponseDto
            {
                Succeeded = false,
                Errors = result.Errors.Select(e => e.Description).ToArray()
            };

        await _userManager.AddToRoleAsync(user, role);
        var token = _jwtTokenGenerator.GenerateToken(user, [role]);

        return new AuthResponseDto
        {
            Succeeded = true,
            UserId = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            Token = token,
            Roles = [role],
            Errors = []
        };
    }

    public async Task<AuthResponseDto> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            return new AuthResponseDto
            {
                Succeeded = false,
                Errors = ["Invalid email or password."]
            };

        var result = await _signInManager
            .CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

        if (!result.Succeeded)
            return new AuthResponseDto
            {
                Succeeded = false,
                Errors = ["Invalid email or password."]
            };

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenGenerator.GenerateToken(user, roles);

        return new AuthResponseDto
        {
            Succeeded = true,
            UserId = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            Token = token,
            Roles = [.. roles],
            Errors = []
        };
    }

    public async Task<IReadOnlyList<UserDto>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.AsNoTracking().ToListAsync();

        var result = new List<UserDto>(users.Count);
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            result.Add(new UserDto
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Roles = [.. roles]
            });
        }

        return result;
    }
}
