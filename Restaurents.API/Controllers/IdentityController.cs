using Microsoft.AspNetCore.Mvc;
using MediatR;
using Restaurants.Application.Users.Commands.UpdateUser;
using Microsoft.AspNetCore.Authorization;
using Restaurants.Domain.Constants;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restaurants.Application.Users.Commands.RemoveUserRole;
namespace Restaurents.API.Controllers;


[ApiController]
[Route("api/identity")]
[Authorize]
public class IdentityController (IMediator mediator) : ControllerBase
{
    [HttpPatch("user")]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserCommand updateUserCommand)
    {
        await mediator.Send(updateUserCommand);

        return NoContent();
    }

    [HttpPost("userRole")]
    [Authorize(Roles = UserRoles.admin)]
    public async Task<IActionResult> AssignUserRoles(AssignUserRoleCommand assignUserRole)
    {
        await mediator.Send(assignUserRole);

        return Ok();
    }

    [HttpDelete("removeUserRole")]
    [Authorize(Roles = UserRoles.admin)]
    public async Task<IActionResult> RemoveUserRoles(RemoveUserRoleCommand removeUserRole)
    {
        await mediator.Send(removeUserRole);

        return NoContent();
    }

} 