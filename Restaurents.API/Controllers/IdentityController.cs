using Microsoft.AspNetCore.Mvc;
using MediatR;
using Restaurants.Application.Users.Commands.UpdateUser;
using Microsoft.AspNetCore.Authorization;
namespace Restaurents.API.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController (IMediator mediator) : ControllerBase
{
    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserCommand updateUserCommand)
    {
        await mediator.Send(updateUserCommand);

        return NoContent();
    }
} 