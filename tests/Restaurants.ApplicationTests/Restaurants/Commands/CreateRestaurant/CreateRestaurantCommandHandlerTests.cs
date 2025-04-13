using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandHandlerTests
{
    [Fact()]
    public async Task Handler_ForCreateRestaurant_ReturnCorrectAction()
    {
        var createCommand = new CreateRestaurantCommand();
        var restaurant = new Restaurant();

        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<Restaurant>(createCommand)).Returns(restaurant);

        var restaurantRepoMock = new Mock<IRestaurantsRepository>();
        
        restaurantRepoMock.Setup(r => r.Create(It.IsAny<Restaurant>())).ReturnsAsync(1);

        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("owner-id", "testuser@test.com", [], null, null, null);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        
        var commandHandler = new CreateRestaurantCommandHandler(
            loggerMock.Object,
            mapperMock.Object,
            restaurantRepoMock.Object,
            userContextMock.Object);

        

        //act
        var result = await commandHandler.Handle(createCommand, CancellationToken.None);

        //assess
        result.Should().Be(1);
        restaurant.OwnerId.Should().Be("owner-id");
        restaurantRepoMock.Verify(r => r.Create(restaurant), Times.Once);
    }
}