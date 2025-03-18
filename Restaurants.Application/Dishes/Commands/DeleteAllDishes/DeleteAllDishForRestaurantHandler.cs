using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Dishes.Commands.DeleteAllDishes;

internal class DeleteAllDishForRestaurantHandler(ILogger<DeleteAllDishForRestaurantHandler> logger,
    IRestaurantsRepository restaurantsRepository, IDishRepository dishRepository,
    IRestaurantAuthorizationService restaurantAuthorization) 
        : IRequestHandler<DeleteAllDishForRestaurantCommand>
{
    public async Task Handle(DeleteAllDishForRestaurantCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete all Dishes from restaurant {@Id}", command.RestaurantId);
        var restaurant = await restaurantsRepository.GetSpecificAsync(command.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), command.RestaurantId.ToString());

        if (!restaurantAuthorization.Authorize(restaurant, ResourceOperations.Delete))
            throw new ForbidException();

        await dishRepository.DeleteAll(restaurant.Dishes);
    }
}
