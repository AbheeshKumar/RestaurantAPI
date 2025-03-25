using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Query.GetAllRestaurants
{
    public class GetAllRestaurantsQuery : IRequest<PageResult<RestaurantDto>>
    {
        public string? SearchParam { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
    }
}
