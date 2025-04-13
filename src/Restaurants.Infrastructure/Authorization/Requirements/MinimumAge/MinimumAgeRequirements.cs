using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumAge;

public class MinimumAgeRequirement(int age) : IAuthorizationRequirement
{
    public int minimumAge { get; } = age;
}