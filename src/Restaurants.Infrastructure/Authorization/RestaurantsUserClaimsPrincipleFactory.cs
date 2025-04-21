using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Authorization;

public class RestaurantsUserClaimsPrincipleFactor(
    IOptions<IdentityOptions> options,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager
    ) : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
{
    public async override Task<ClaimsPrincipal> CreateAsync(User user) {

        var id = await GenerateClaimsAsync(user);

        if (user.Nationality != null) {
            id.AddClaim(new Claim(ClaimTypes.Nationality, user.Nationality));
        }

        if (user.DateOfBirth != null)
        {
            id.AddClaim(new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
        }

        return new ClaimsPrincipal(id);
    }
}