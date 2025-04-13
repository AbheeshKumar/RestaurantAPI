using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDish;

public class DeleteDishForRestaurantCommand(int restaurantId, int dishId) : IRequest 
{
    public int restaurantId { get; set; } = restaurantId;
    public int dishId { get; set; } = dishId;
}
