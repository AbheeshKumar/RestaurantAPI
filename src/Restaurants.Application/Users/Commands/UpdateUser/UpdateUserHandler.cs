using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UpdateUser;

internal class UpdateUserHandler(ILogger<UpdateUserHandler> logger, IUserContext userContext,
    IUserStore<User> userStore) : IRequestHandler<UpdateUserCommand> 
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("Updating User: {userId} with {@request}", user!.Id, request);

        var dbUser = await userStore.FindByIdAsync(user.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(user), user.Id);

        dbUser.Nationality = request.Nationality;
        dbUser.DateOfBirth = request.DateofBirth;

        await userStore.UpdateAsync(dbUser, cancellationToken);

    }
}
