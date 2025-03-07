using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Query.GetAllRestaurants;
using Restaurants.Application.Restaurants.Query.GetSpecificRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurents.API.Controllers;

[ApiController]
[Route("api/restaurants")]
[Authorize]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }

    [HttpGet("{RestaurantId}")]
    [AllowAnonymous]
    public async Task<ActionResult<Restaurant>> GetRestaurantById([FromRoute] int RestaurantId)
    {
        var restaurant = await mediator.Send(new GetSpecificRestaurantQuery(RestaurantId));

        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand createRestaurant) 
    {
        int id = await mediator.Send(createRestaurant);
        return CreatedAtAction(nameof(GetRestaurantById), new { RestaurantId = id }, null);
    }

    [HttpDelete("{RestaurantId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int RestaurantId)
    {
        await mediator.Send(new DeleteRestaurantCommand(RestaurantId));

       return NoContent();
    }

    [HttpPatch("{RestaurantId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] int RestaurantId, [FromBody] UpdateRestaurantCommand command)
    {
        command.Id = RestaurantId;
        await mediator.Send(command);

        return NoContent();   
    }
}


