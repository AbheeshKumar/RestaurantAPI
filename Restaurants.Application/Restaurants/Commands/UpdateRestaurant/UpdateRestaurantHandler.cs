
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

internal class UpdateRestaurantHandler(ILogger<UpdateRestaurantHandler> logger, 
    IRestaurantsRepository restaurantsRepository, 
    IMapper mapper,
    IRestaurantAuthorizationService restaurantAuthorization) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Logging Restaurant {command.Id}");
        var restaurant = await restaurantsRepository.GetSpecificAsync(command.Id)
            ?? throw new NotFoundException(nameof(Restaurant), command.Id.ToString());

        if (!restaurantAuthorization.Authorize(restaurant, ResourceOperations.Update))
            throw new ForbidException();

        mapper.Map(command, restaurant);

        await restaurantsRepository.SaveChanges();
    }
}
