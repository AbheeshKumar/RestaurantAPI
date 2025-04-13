
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger, IUserContext userContext) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperations resourceOperation)
    {
        var user = userContext.GetCurrentUser();

        if (resourceOperation == ResourceOperations.Read || resourceOperation == ResourceOperations.Create)
        {
            logger.LogInformation("Read/Create Operation occurred");
            return true;
        }

        if (resourceOperation == ResourceOperations.Delete && user.IsRole(UserRoles.admin))
        {
            logger.LogInformation("Delete Operation perfomed by admin");
            return true;
        }

        if ((resourceOperation == ResourceOperations.Delete || resourceOperation == ResourceOperations.Update) && user.Id == restaurant.OwnerId)
        {
            logger.LogInformation("Delete Operation perfomed by owner");
            return true;
        }
        return false;
    }

}
