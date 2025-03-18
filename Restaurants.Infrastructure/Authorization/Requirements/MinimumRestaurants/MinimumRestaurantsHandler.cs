using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumRestaurants
{
    internal class MinimumRestaurantsHandler(ILogger<MinimumRestaurantsHandler> logger, IUserContext userContext, IRestaurantsRepository restaurantsRepository) 
        : AuthorizationHandler<MinimumRestaurantsRequriement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantsRequriement requriement)
        {
            var user = userContext.GetCurrentUser();

            var restaurants = await restaurantsRepository.GetAllAsync();

            var userRestaurants = restaurants.Count(r => r.OwnerId == user!.Id);

            if (userRestaurants >= requriement.minimumRestaurants)
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