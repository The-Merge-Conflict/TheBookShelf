using System.Text.Json;
using DLMS.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DLMS.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception on {Method} {Path}",
                    context.Request.Method, context.Request.Path);

                await HandleAsync(context, ex);
            }
        }

        private static async Task HandleAsync(HttpContext context, Exception exception)
        {
            var (status, title, errors) = exception switch
            {
                NotFoundException nfe => (
                    StatusCodes.Status404NotFound,
                    "Resource Not Found",
                    new Dictionary<string, string[]>
                    { ["general"] = [nfe.Message] }),

                Application.Common.Exceptions.ValidationException ve => (
                    StatusCodes.Status400BadRequest,
                    "Validation Failed",
                    ve.Errors),

                UnauthorizedAccessException => (
                    StatusCodes.Status401Unauthorized,
                    "Unauthorized",
                    new Dictionary<string, string[]>
                    { ["general"] = ["You are not authorized to perform this action."] }),

                _ => (
                    StatusCodes.Status500InternalServerError,
                    "Internal Server Error",
                    new Dictionary<string, string[]>
                    { ["general"] = ["An unexpected error occurred. Please try again later."] })
            };

            var problem = new ValidationProblemDetails(errors)
            {
                Status = status,
                Title = title,
                Type = $"https://httpstatuses.com/{status}"
            };

            context.Response.StatusCode = status;
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(problem,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }));
        }
    }
}