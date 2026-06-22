using DLMS.Infrastructure;
using DLMS.Application;
using Microsoft.OpenApi.Models;
using Serilog;
using DLMS.API.Converters;
using DLMS.API.Middleware;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, config) =>
        config.ReadFrom.Configuration(context.Configuration)
              .ReadFrom.Services(services)
              .Enrich.FromLogContext()
              .Enrich.WithMachineName()
              .Enrich.WithThreadId());

    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    // CORS — allow the Next.js admin app (different origin) to call this API
    // from the browser. Without this every request from http://localhost:3000
    // is blocked by the browser's same-origin policy.
    const string AdminCorsPolicy = "AdminCors";
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(AdminCorsPolicy, policy =>
        {
            var allowedOrigins = builder.Configuration
                .GetSection("Cors:AllowedOrigins").Get<string[]>()
                ?? new[] { "http://localhost:3000" };

            policy.WithOrigins(allowedOrigins)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });

    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new LanguageCodeJsonConverter());
        });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "DLMS API",
            Version = "v1"
        });

        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Paste your JWT token. Swagger will send it as: Bearer {token}",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        };

        options.AddSecurityDefinition("Bearer", securityScheme);
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { securityScheme, Array.Empty<string>() }
        });
    });

    var app = builder.Build();
    app.UseMiddleware<ExceptionHandlingMiddleware>();

    await app.Services.SeedRolesAsync();

    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate =
            "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
    });

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "DLMS API v1");
            options.DisplayRequestDuration();
        });
    }

    app.UseHttpsRedirection();
    app.UseCors(AdminCorsPolicy);
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    Log.Information("DLMS API starting in {Environment} mode",
        app.Environment.EnvironmentName);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly during startup");
}
finally
{
    Log.CloseAndFlush();
}
