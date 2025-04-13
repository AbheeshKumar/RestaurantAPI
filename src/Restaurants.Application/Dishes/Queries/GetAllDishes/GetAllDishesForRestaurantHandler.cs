using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Queries.GetAllDishes;

public class GetAllDishesForRestaurantHandler(ILogger<GetAllDishesForRestaurantHandler> logger,
    IRestaurantsRepository restaurantRepository, IMapper mapper) : IRequestHandler<GetAllDishesForRestaurantQuery, IEnumerable<DishesDto>>
{
    public async Task<IEnumerable<DishesDto>> Handle(GetAllDishesForRestaurantQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all dishes from restaurant {@Id}", query.RestaurantId);
        var restaurant = await restaurantRepository.GetSpecificAsync(query.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), query.RestaurantId.ToString());

        var dishDtos = mapper.Map<IEnumerable<DishesDto>>(restaurant.Dishes);

        return dishDtos;
    }
}
