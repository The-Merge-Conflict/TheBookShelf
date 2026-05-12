using System.Text;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Interfaces;
using DLMS.Infrastructure.Identity;
using DLMS.Infrastructure.Persistence;
using DLMS.Infrastructure.Repositories;
using DLMS.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DLMS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ── Database ─────────────────────────────────────────────────────────
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sql => sql.MigrationsAssembly(
                    typeof(ApplicationDbContext).Assembly.FullName)));

        // Expose ApplicationDbContext as IApplicationDbContext to the Application layer
        services.AddScoped<IApplicationDbContext>(
            sp => sp.GetRequiredService<ApplicationDbContext>());

        // ── Identity ─────────────────────────────────────────────────────────
        services
            .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password policy — matches RegisterCommandValidator rules
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;

                // Lockout
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

                // User
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // ── JWT Authentication ────────────────────────────────────────────────
        var jwt = configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwt["Key"]
            ?? throw new InvalidOperationException(
                "JWT Key is missing from configuration."));

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // set true in production
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwt["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwt["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero   // no grace period on expiry
                };
            });

        // ── Repositories & Unit of Work ───────────────────────────────────────
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // ── Application Services ──────────────────────────────────────────────
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<IMediaProcessingService, MediaProcessingService>();

        // ── Current User (needed by Member 3's handlers) ──────────────────────
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }

    /// <summary>
    /// Seeds the three application roles into the database.
    /// Call this once from Program.cs after app.Build().
    /// </summary>
    public static async Task SeedRolesAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var roleManager = scope.ServiceProvider
                                     .GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = ["Admin", "Editor", "Viewer"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}