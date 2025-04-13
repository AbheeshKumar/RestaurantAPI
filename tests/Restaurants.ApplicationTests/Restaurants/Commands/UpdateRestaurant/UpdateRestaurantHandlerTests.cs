using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests;

public class UpdateRestaurantHandlerTests
{
    private readonly Mock<ILogger<UpdateRestaurantHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IRestaurantsRepository> _restaurantRepoMock;
    private readonly Mock<IRestaurantAuthorizationService> _authorizationServiceMock;
    private readonly UpdateRestaurantHandler _updateHandler;
    public UpdateRestaurantHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateRestaurantHandler>>();
        _mapperMock = new Mock<IMapper>();
        _restaurantRepoMock = new Mock<IRestaurantsRepository>();
        _authorizationServiceMock = new Mock<IRestaurantAuthorizationService>();
        _updateHandler = new UpdateRestaurantHandler(
            _loggerMock.Object,
            _restaurantRepoMock.Object,
            _mapperMock.Object,
            _authorizationServiceMock.Object
            );
    }

    [Fact()]
    public async Task Handler_ForUpdateRestaurant_ReturnCorrectAction()
    {
        var updateCommand = new UpdateRestaurantCommand()
        {
            Id = 1,
            Name = "Test 2",
            Description = "Test 2 Description",
            HasDelivery = false,
        };

        var restaurant = new Restaurant()
        {
            Id = 1,
            Name = "Test Drink",
            Description = "Test Description",
            Category = "Test",
            HasDelivery = true,
            ContactEmail = "owner@gmail.com",
            ContactNumber = "02222",
        };

        _mapperMock.Setup(m => m.Map(updateCommand, restaurant)).Returns(restaurant);
       
        _restaurantRepoMock.Setup(r => r.GetSpecificAsync(updateCommand.Id)).ReturnsAsync(restaurant);
        _restaurantRepoMock.Setup(r => r.SaveChanges());

        _authorizationServiceMock.Setup(s => s.Authorize(restaurant, ResourceOperations.Update))
            .Returns(true);

        await _updateHandler.Handle(updateCommand, CancellationToken.None);

        _restaurantRepoMock.Verify(r => r.SaveChanges(), Times.Once);
        _mapperMock.Verify(r => r.Map(updateCommand, restaurant), Times.Once);
    }

    [Fact()]
    public void Handle_ForUpdateRestaurantException_ReturnNullException()
    {
        var updateCommand = new UpdateRestaurantCommand()
        {
            Id = 1,
        };

        _restaurantRepoMock.Setup(r => r.GetSpecificAsync(updateCommand.Id))
            .ReturnsAsync((Restaurant?)null);

        Func<Task> act = async() => await _updateHandler.Handle(updateCommand, CancellationToken.None);

        act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Restaurant with {updateCommand.Id} doesn't exist");
    }

    [Fact()]
    public async Task Handle_ForUnAuthorizedUser_ReturnForbidException()
    {
        var restaurant = new Restaurant
        {
            Id = 2
        };
        var updateCommand = new UpdateRestaurantCommand
        {
            Id = 2
        };

        _restaurantRepoMock.Setup(r => r.GetSpecificAsync(updateCommand.Id))
            .ReturnsAsync(restaurant);

        _authorizationServiceMock.Setup(a => a.Authorize(restaurant, ResourceOperations.Update))
            .Returns(false);

        Func<Task> act = async () => await _updateHandler.Handle(updateCommand, CancellationToken.None);

        await act.Should().ThrowAsync<ForbidException>();
    }
}