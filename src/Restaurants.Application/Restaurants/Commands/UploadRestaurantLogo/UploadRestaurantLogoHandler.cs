using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.UploadFile;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UploadRestaurantLogo;

public class UploadRestaurantLogoHandler(ILogger<UploadRestaurantLogoHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IRestaurantAuthorizationService authorizationService,
    IBlobStorageService blobStorageService
    ) : IRequestHandler<UploadRestaurantLogoCommand>
{
    public async Task Handle(UploadRestaurantLogoCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Adding logo to restaurant {command.RestaurantId}");
        var restaurant = await restaurantsRepository.GetSpecificAsync(command.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), command.RestaurantId.ToString());

        if (!authorizationService.Authorize(restaurant, ResourceOperations.Update))
            throw new ForbidException();

        var blobUrl = await blobStorageService.UploadToBlobAsync(command.File, command.FileName);

        restaurant.LogoUrl = blobUrl;

        await restaurantsRepository.SaveChanges();
    }
}
