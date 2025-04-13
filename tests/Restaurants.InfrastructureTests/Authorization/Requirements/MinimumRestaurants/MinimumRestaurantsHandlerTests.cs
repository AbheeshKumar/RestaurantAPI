using Xunit;
using Restaurants.Application.Users;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumRestaurants.Tests
{
    public class MinimumRestauraHantsHandlerTests
    {
        [Fact()]
        public async Task HandleRequirementAsync_UserOwnsMultipleRestaurants_ShouldSucceed()
        {
            var user = new CurrentUser("1", "test@gmail.com", [], null, null, []);
            var userContext = new Mock<IUserContext>();

            userContext.Setup(u => u.GetCurrentUser()).Returns(user);

            var restaurants = new List<Restaurant>() {
                new()
                {
                    OwnerId = user.Id
                },
                new()
                {
                    OwnerId = user.Id
                },
                new()
                {
                    OwnerId = "2"
                },
            };

            var restaurantRepo = new Mock<IRestaurantsRepository>();
            restaurantRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurants);

            var minRestaurantsReq = new MinimumRestaurantsRequriement(2);

            var minRestaurantHandler = new MinimumRestaurantsHandler(null, userContext.Object, restaurantRepo.Object);
            var authorizationContext = new AuthorizationHandlerContext([minRestaurantsReq], null, null);

            //act
            await minRestaurantHandler.HandleAsync(authorizationContext);

            //Assert
            authorizationContext.HasSucceeded.Should().BeTrue();

        }
        [Fact()]
        public async Task HandleRequirementAsync_UserOwnsMultipleRestaurants_ShouldFail()
        {
            var user = new CurrentUser("1", "test@gmail.com", [], null, null, []);
            var userContext = new Mock<IUserContext>();
            var logger = new Mock<ILogger<MinimumRestaurantsHandler>>();

            userContext.Setup(u => u.GetCurrentUser()).Returns(user);

            var restaurants = new List<Restaurant>() {
                new()
                {
                    OwnerId = user.Id
                },
                new()
                {
                    OwnerId = "2"
                },
            };

            var restaurantRepo = new Mock<IRestaurantsRepository>();
            restaurantRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurants);

            var minRestaurantsReq = new MinimumRestaurantsRequriement(2);

            var minRestaurantHandler = new MinimumRestaurantsHandler(logger.Object, userContext.Object, restaurantRepo.Object);
            var authorizationContext = new AuthorizationHandlerContext([minRestaurantsReq], null, null);

            //act
            await minRestaurantHandler.HandleAsync(authorizationContext);
            
            //Assert
            authorizationContext.HasFailed.Should().BeTrue();

        }
    }
}