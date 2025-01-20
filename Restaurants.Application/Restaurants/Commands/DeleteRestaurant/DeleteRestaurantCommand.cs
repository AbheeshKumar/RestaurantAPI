
using MediatR;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommand(int RestaurentId) : IRequest
{
    public int Id { get; set; } = RestaurentId;
}
