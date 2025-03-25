
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Query.GetAllRestaurants;

internal class GetAllRestaurantsHandler(ILogger<GetAllRestaurantsHandler> logger, 
    IMapper mapper, IRestaurantsRepository restaurantsRepository) 
    : IRequestHandler<GetAllRestaurantsQuery, PageResult<RestaurantDto>>
{
    public async Task<PageResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    { 
        logger.LogInformation("Fetching all Restaurants");
        var (restaurants, totalCount) = await restaurantsRepository.GetAllMatchingAsync(
            request.SearchParam, request.pageSize, request.pageNumber
            );

        var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        var result = new PageResult<RestaurantDto>(restaurantsDtos, totalCount, request.pageSize, request.pageNumber);
        return result;
    }

}
