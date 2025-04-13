using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;
using Xunit;

namespace Restaurants.Application.Restaurants.Dtos.Tests;

public class RestaurantProfileTests
{
    private IMapper _mapper;
    public RestaurantProfileTests()
    {
        var map = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RestaurantProfile>();
        });

        _mapper = map.CreateMapper();
    }
    [Fact()]
    public void Mapper_RestaurantToRestaurantDto_ReturnCorrectMapping()
    {
        var restaurant = new Restaurant()
        {
            Id = 1,
            Name = "Test Drink",
            Description = "Test Description",
            Category = "Test",
            HasDelivery = true,
            ContactEmail = "owner@gmail.com",
            ContactNumber = "02222",
            Address = new Address()
            {
                PostalCode = "55-420",
                City = "Test",
                Street = "Test Road",
            }
        };

        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

        restaurantDto.Should().NotBeNull();
        restaurantDto.Id.Should().Be(restaurant.Id);
        restaurantDto.Name.Should().Be(restaurant.Name);
        restaurantDto.Description.Should().Be(restaurant.Description);
        restaurantDto.Category.Should().Be(restaurant.Category);
        restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
        restaurantDto.City.Should().Be(restaurant.Address.City);
        restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);
        restaurantDto.Street.Should().Be(restaurant.Address.Street);
    }

    [Fact()]
    public void Mapper_UpdateRestaurantToRestaurant_ReturnCorrectMapping()
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
            Address = new Address()
            {
                PostalCode = "55-420",
                City = "Test",
                Street = "Test Road",
            }
        };

        restaurant = _mapper.Map<Restaurant>(updateCommand);

        restaurant.Id.Should().Be(updateCommand.Id);
        restaurant.Name.Should().Be(updateCommand.Name);
        restaurant.Description.Should().Be(updateCommand.Description);
        restaurant.HasDelivery.Should().Be(updateCommand.HasDelivery);
    }

    [Fact()]
    public void Mapper_CreateRestaurantToRestaurant_ReturnCorrectMapping()
    {
        var createCommand = new CreateRestaurantCommand()
        {
            Name = "Test Drink",
            Description = "Test Description",
            Category = "Test",
            HasDelivery = true,
            ContactEmail = "owner@gmail.com",
            ContactNumber = "02222",
            PostalCode = "55-420",
            City = "Test",
            Street = "Test Road"
        };

        var restaurant = _mapper.Map<Restaurant>(createCommand);

        restaurant.Name.Should().Be(createCommand.Name);
        restaurant.Description.Should().Be(createCommand.Description);
        restaurant.Category.Should().Be(createCommand.Category);
        restaurant.HasDelivery.Should().Be(createCommand.HasDelivery);
        restaurant.ContactEmail.Should().Be(createCommand.ContactEmail);
        restaurant.ContactNumber.Should().Be(createCommand.ContactNumber);
        restaurant.Address.City.Should().Be(createCommand.City);
        restaurant.Address.PostalCode.Should().Be(createCommand.PostalCode);
        restaurant.Address.Street.Should().Be(createCommand.Street);
    }
}