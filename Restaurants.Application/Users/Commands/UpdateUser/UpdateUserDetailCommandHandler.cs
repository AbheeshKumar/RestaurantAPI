using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
namespace Restaurants.Application.Users.Commands.UpdateUser;

public class UpdateUserDetailCommandHandler(ILogger<UpdateUserDetailCommandHandler> logger,
    IUserContext userContext, IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand Request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("Updating User {@userId} with {@Request}", user!.Id, Request);

        var dbUser = await userStore.FindByIdAsync(user.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(User), user.Id);

        dbUser.Nationality = Request.Nationality;
        dbUser.DateOfBirth = Request.DateOfBirth;

        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}
