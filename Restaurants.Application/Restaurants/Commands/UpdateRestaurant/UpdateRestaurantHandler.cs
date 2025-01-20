
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

internal class UpdateRestaurantHandler(ILogger<UpdateRestaurantHandler> logger, 
    IRestaurantsRepository restaurantsRepository, 
    IMapper mapper) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Logging Restaurant {command.Id}");
        var restaurant = await restaurantsRepository.GetSpecificAsync(command.Id)
            ?? throw new NotFoundException(nameof(Restaurant), command.Id.ToString());

        mapper.Map(command, restaurant);

        await restaurantsRepository.SaveChanges();
    }
}
