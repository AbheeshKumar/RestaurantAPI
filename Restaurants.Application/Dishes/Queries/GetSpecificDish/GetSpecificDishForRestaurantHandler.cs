using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Queries.GetSpecificDish;

public class GetSpecificDishForRestaurantHandler(ILogger<GetSpecificDishForRestaurantHandler> logger,
    IMapper mapper, IRestaurantsRepository restaurantsRepository)
        : IRequestHandler<GetSpecificDishForRestaurantQuery, DishesDto>
{
    public async Task<DishesDto> Handle(GetSpecificDishForRestaurantQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Restaurant {@Id} with Dish {@Id}", query.restaurantId, query.dishId);
        var restaurant = await restaurantsRepository.GetSpecificAsync(query.restaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), query.restaurantId.ToString());

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == query.dishId)
            ?? throw new NotFoundException(nameof(Dish), query.dishId.ToString());

        var dishDto = mapper.Map<DishesDto>(dish);

        return dishDto;
    }
}
