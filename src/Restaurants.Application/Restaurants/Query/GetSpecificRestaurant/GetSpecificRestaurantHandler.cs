using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Restaurants.Query.GetSpecificRestaurant;

internal class GetSpecificRestaurantHandler(ILogger<GetSpecificRestaurantHandler> logger,
    IMapper mapper, 
    IRestaurantsRepository restaurantsRepository,
    IBlobStorageService blobStorageService
    ) 
    : IRequestHandler<GetSpecificRestaurantQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetSpecificRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Getting Restaurant by {request.Id}");
        var restaurant = await restaurantsRepository.GetSpecificAsync(request.Id)
            ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

        restaurantDto.LogoSasUri = blobStorageService.GetBlobUrl(restaurant.LogoUrl);
        return restaurantDto;

    }
}
