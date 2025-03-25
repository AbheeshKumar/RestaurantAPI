using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumRestaurants
{
    internal class MinimumRestaurantsHandler(ILogger<MinimumRestaurantsHandler> logger, IUserContext userContext, IRestaurantsRepository restaurantsRepository) 
        : AuthorizationHandler<MinimumRestaurantsRequriement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantsRequriement requriement)
        {
            var user = userContext.GetCurrentUser();
            if (user == null)
            {
                throw new NotFoundException(nameof(user), "User Not Found");
            }

            var restaurants = await restaurantsRepository.GetAllAsync();
            if (restaurants == null)
            {
                throw new NotFoundException(nameof(restaurants), "Restaurant not found.");
            }

            var userRestaurants = restaurants.Where(r => r.OwnerId == user!.Id);

            if (!userRestaurants.Any())
                throw new NotFoundException(nameof(user), user!.Id.ToString());

            if (userRestaurants.Count() >= requriement.minimumRestaurants)
            {
                context.Succeed(requriement);
            }
            else
            {
                logger.LogError("User doesn't have enough restaurants");
                context.Fail();
            }
        }
    }
}