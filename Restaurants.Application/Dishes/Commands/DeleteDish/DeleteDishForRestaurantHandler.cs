using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Commands.DeleteDish;

internal class DeleteDishForRestaurantHandler(ILogger<DeleteDishForRestaurantHandler> logger,
    IRestaurantsRepository restaurantsRepository, IDishRepository dishRepository) : IRequestHandler<DeleteDishForRestaurantCommand>
{
    public async Task Handle(DeleteDishForRestaurantCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Dish {@Id} with Restaurant {@Id}", command.dishId, command.restaurantId);

        var restaurant = await restaurantsRepository.GetSpecificAsync(command.restaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), command.restaurantId.ToString());

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == command.dishId)
            ?? throw new NotFoundException(nameof(Dish), command.dishId.ToString());

        await dishRepository.DeleteSpecific(dish);
    }
}
