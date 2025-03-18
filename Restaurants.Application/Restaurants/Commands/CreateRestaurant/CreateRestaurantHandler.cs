using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, 
    IMapper mapper, IRestaurantsRepository restaurantsRepository, IUserContext userContext) 
    : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("{Email} Creating a restaurant", user.Email);
        var restaurant = mapper.Map<Restaurant>(request);

        restaurant.OwnerId = user.Id;

        int Id = await restaurantsRepository.Create(restaurant);
        return Id;
    }
}
