using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetSpecificDish;

public class GetSpecificDishForRestaurantQuery(int restaurantId, int dishId) : IRequest<DishesDto>
{
    public int restaurantId { get; } = restaurantId;
    public int dishId { get; } = dishId;
}
