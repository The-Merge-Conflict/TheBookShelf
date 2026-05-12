using System.Security.Claims;
using DLMS.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DLMS.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        public string? UserId =>
            _httpContextAccessor.HttpContext?.User
                .FindFirstValue(ClaimTypes.NameIdentifier);

        public string? UserName =>
            _httpContextAccessor.HttpContext?.User
                .FindFirstValue(ClaimTypes.Name);
    }
}