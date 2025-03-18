using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.RemoveUserRole;

internal class RemoveUserRoleHandler(ILogger<RemoveUserRoleHandler> logger, UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<RemoveUserRoleCommand>
{
    public async Task Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing user role {@request}", request);
        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(userManager), request.UserEmail);

        var role = await roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException(nameof(roleManager), request.RoleName);

        await userManager.RemoveFromRoleAsync(user, role.Name!);
    }
}
