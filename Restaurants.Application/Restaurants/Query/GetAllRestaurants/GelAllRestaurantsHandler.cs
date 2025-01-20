
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Query.GetAllRestaurants;

internal class GetAllRestaurantsHandler(ILogger<GetAllRestaurantsHandler> logger, 
    IMapper mapper, IRestaurantsRepository restaurantsRepository) 
    : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    { 
        logger.LogInformation("Fetching all Restaurants");
        var restaurants = await restaurantsRepository.GetAllAsync();
        var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        return restaurantsDtos;
    }

}
