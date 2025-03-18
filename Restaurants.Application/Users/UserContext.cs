using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.Users;

public interface IUserContext
{
    public CurrentUser? GetCurrentUser();
}

internal class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User;

        if (user == null)
        {
            throw new InvalidOperationException("User Context is not present");
        }

        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return null;
        }

        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var userEmail = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var userRoles = user.FindAll(c => c.Type == ClaimTypes.Role)!.Select(r => r.Value);
        var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
        var dobString = user.FindFirst(c => c.Type == "DateOfBirth")?.Value;

        var dob = dobString == null ?
            (DateOnly?) null :
            DateOnly.ParseExact(dobString, "yyyy-MM-dd");

        var ownedRestaurants = user.FindAll(c => c.Type == "OwnedRestaurants").Select(r => r.Value);

        return new CurrentUser(userId, userEmail, userRoles, nationality, dob, ownedRestaurants);

    }
}
