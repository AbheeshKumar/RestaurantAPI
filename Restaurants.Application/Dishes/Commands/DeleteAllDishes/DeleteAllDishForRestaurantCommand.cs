using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteAllDishes;

public class DeleteAllDishForRestaurantCommand(int restaurantId) : IRequest
{
    public int RestaurantId { get; set; } = restaurantId;
}
