using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDishes;

public class CreateDishesHandler(IMapper mapper, ILogger<CreateDishesHandler> logger,
    IRestaurantsRepository restaurantsRepository, IDishRepository dishRepository)
    : IRequestHandler<CreateDishesCommand, int>
{
    public async Task<int> Handle(CreateDishesCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new dish: {@DishRequest}", command);

        var restaurant = restaurantsRepository.GetSpecificAsync(command.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), command.RestaurantId.ToString());

        var dish = mapper.Map<Dish>(command);

        int Id = await dishRepository.Create(dish);

        return Id;
    }
}
