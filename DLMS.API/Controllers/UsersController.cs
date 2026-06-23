using DLMS.Application.Features.Users.Queries.GetAllUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DLMS.API.Controllers;

[Authorize(Roles = "Admin")]
public class UsersController : BaseApiController
{
    /// <summary>Get all registered users with their roles.</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await Mediator.Send(new GetAllUsersQuery(), ct));
}
