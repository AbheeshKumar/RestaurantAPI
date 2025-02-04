using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands.UpdateUser;

namespace Restaurents.API.Controllers
{
    [ApiController]
    [Route("api/identity")]
    [Authorize]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("Update")]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand updateUserDetails)
        {
            await mediator.Send(updateUserDetails);

            return NoContent();
        }
    }
}
