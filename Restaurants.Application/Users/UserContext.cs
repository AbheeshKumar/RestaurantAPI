using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.Users;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User;

        if (user == null)
        {
            throw new InvalidOperationException("User Context is empty");
        }

        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            throw new InvalidOperationException("Not Authenticated");
        }

        var userId = user.FindFirst(u => u.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(u => u.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(u => u.Type == ClaimTypes.Role).Select(u => u.Value);
         
        return new CurrentUser(userId, email, roles);
    }
}
