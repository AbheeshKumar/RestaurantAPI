using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

internal class DeleteRestaurantHandler(ILogger<DeleteRestaurantHandler> logger,
    IRestaurantsRepository restaurantsRepository) 
    : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting Restaurant with {request.Id}");

        var restaurant = await restaurantsRepository.GetSpecificAsync(request.Id)
            ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        

        await restaurantsRepository.Delete(restaurant);

    }
}
