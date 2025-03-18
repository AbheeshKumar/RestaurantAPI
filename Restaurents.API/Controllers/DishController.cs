using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDishes;
using Restaurants.Application.Dishes.Commands.DeleteAllDishes;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetAllDishes;
using Restaurants.Application.Dishes.Queries.GetSpecificDish;
using Restaurants.Infrastructure.Authorization;

namespace Restaurents.API.Controllers;

[ApiController]
[Route("api/restaurants/{restaurantId}/dishes")]
[Authorize]
public class DishController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = PolicyNames.Atleast20)]
    public async Task<IActionResult> CreateDishes([FromRoute] int restaurantId, CreateDishesCommand command)
    {
        command.RestaurantId = restaurantId;
        int dishId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetSpecificDishForRestaurant), new { restaurantId, dishId}, null);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishesDto>>> GetAllDishesForRestaurant([FromRoute] int restaurantId)
    {
        var dishes = await mediator.Send(new GetAllDishesForRestaurantQuery(restaurantId));
        return Ok(dishes);
    }

    [HttpGet("{dishId}")]
    public async Task<ActionResult<DishesDto>> GetSpecificDishForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        var dish = await mediator.Send(new GetSpecificDishForRestaurantQuery(restaurantId, dishId));
        return Ok(dish);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAllDishForRestaurant([FromRoute] int restaurantId)
    {
        await mediator.Send(new DeleteAllDishForRestaurantCommand(restaurantId));

        return NoContent();
    }

    [HttpDelete("{dishId}")]
    public async Task<IActionResult> DeleteDishForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        await mediator.Send(new DeleteDishForRestaurantCommand(restaurantId, dishId));

        return NoContent();
    }
}
