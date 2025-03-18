
using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumRestaurants;

public class MinimumRestaurantsRequriement(int minimumRestaurants) : IAuthorizationRequirement
{
    public int minimumRestaurants { get; } = minimumRestaurants;
}
