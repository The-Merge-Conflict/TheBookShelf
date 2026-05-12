using DLMS.Application.Features.Auth.Commands.Login;
using DLMS.Application.Features.Auth.Commands.Register;
using Microsoft.AspNetCore.Mvc;

namespace DLMS.API.Controllers
{
    public class AuthController : BaseApiController
    {
        /// <summary>Register a new user account.</summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterCommand command,
            CancellationToken ct)
        {
            var result = await Mediator.Send(command, ct);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result);
        }

        /// <summary>Login and receive a JWT token.</summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginCommand command,
            CancellationToken ct)
        {
            var result = await Mediator.Send(command, ct);

            if (!result.Succeeded)
                return Unauthorized(result.Errors);

            return Ok(result);
        }
    }
}