using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumAge;

internal class MinimumAgeHandler(ILogger<MinimumAgeHandler> logger, IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation(@"Ensuring {email} has correct {Date of Birth} to join", user.Email, user.DateOfBirth);

        if (user.DateOfBirth == null)
        {
            logger.LogWarning("User doesn't have a date of birth");
            context.Fail();
            return Task.CompletedTask;
        }

        if (user.DateOfBirth.Value.AddYears(requirement.minimumAge) < DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("User exceeds minimum Age");
            context.Succeed(requirement);
        }
        else
        {
            logger.LogInformation("User is below minimum Age");
            context.Fail();
        }
        return Task.CompletedTask;
    }
}