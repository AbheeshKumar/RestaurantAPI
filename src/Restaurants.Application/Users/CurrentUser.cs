
namespace Restaurants.Application.Users;

public record CurrentUser(string Id, string Email, IEnumerable<string> Roles, string? Nationality, DateOnly? DateOfBirth, IEnumerable<string>? OwnedRestaurants)
{
    public bool IsRole(string role) => Roles.Contains(role);

}
