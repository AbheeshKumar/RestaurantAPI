
using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Query.GetSpecificRestaurant;

public class GetSpecificRestaurantQuery(int RestaurantId) : IRequest<RestaurantDto>
{
    public int Id { get; } = RestaurantId;
}
