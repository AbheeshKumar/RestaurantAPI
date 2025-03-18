namespace Restaurants.Application.Users;

public abstract class UserRoleCommand
{
    public string UserEmail { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}
