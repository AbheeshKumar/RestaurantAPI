using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurents.API.Controllers.Tests;

public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IRestaurantsRepository> _restaurantRepoMock = new();
    public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantsRepository), _ => _restaurantRepoMock.Object));
            });
        });
    }
    [Fact()]
    public async Task GetAll_ValidRequest_Get200Ok()
    {
        var client = _factory.CreateClient();
        var result = await client.GetAsync("/api/restaurants?pageNumber=1&pageSize=10&sortdirection=Desc");

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
    [Fact()]
    public async Task GetAll_InvalidRequest_Get400BadRequest()
    {
        var client = _factory.CreateClient();
        var result = await client.GetAsync("api/restaurants");

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact()]
    public async Task GetRestaurantById_InvalidRequest_Get400NotFound()
    {
        var id = 1122;

        _restaurantRepoMock.Setup(r => r.GetSpecificAsync(id)).ReturnsAsync((Restaurant?)null);

        var client = _factory.CreateClient();

        var result = await client.GetAsync($"api/restaurants/{id}");

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
    [Fact]
    public async Task GetRestaurantById_ValidRequest_Get200Ok()
    {
        var id = 12;
        var restaurant = new Restaurant()
        {
            Id = id,
            Name = "Test Restaurant",
            Description = "Test Description"
        };

        _restaurantRepoMock.Setup(r => r.GetSpecificAsync(id)).ReturnsAsync(restaurant);

        var client = _factory.CreateClient();

        var result = await client.GetAsync($"/api/restaurants/{id}");

        var restaurantDto = await result.Content.ReadFromJsonAsync<RestaurantDto>();

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        restaurantDto.Should().NotBeNull();
        restaurantDto.Name.Should().Be(restaurant.Name);
        restaurantDto.Description.Should().Be(restaurant.Description);
    }
    //[Fact()]
    //public void CreateRestaurantTest()
    //{
    //    Xunit.Assert.Fail("This test needs an implementation");
    //}

    //[Fact()]
    //public void DeleteRestaurantTest()
    //{
    //    Xunit.Assert.Fail("This test needs an implementation");
    //}

    //[Fact()]
    //public void UpdateRestaurantTest()
    //{
    //    Xunit.Assert.Fail("This test needs an implementation");
    //}
}