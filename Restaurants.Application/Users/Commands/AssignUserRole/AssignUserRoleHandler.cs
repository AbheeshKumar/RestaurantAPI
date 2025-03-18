using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.AssignUserRole;

internal class AssignUserRoleHandler(ILogger<AssignUserRoleHandler> logger, UserManager<User> userManager, 
    RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding Role to user {@Request}", request);

        //Find User through Email
        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(User), request.UserEmail);

        //Find Role
        var role = await roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException(nameof(User), request.RoleName);

        //Add ROle to user
        await userManager.AddToRoleAsync(user, role.Name!);
    }
}
