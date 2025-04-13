using Xunit;
using FluentValidation.TestHelper;


namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantDtoValidatorTests
{
    [Fact()]
    public void Validator_ForValidRestaurant_ReturnNoValidationErrors()
    {
        //arrange
        var restaurant = new CreateRestaurantCommand()
        {
            Name = "Test",
            Category = "Italian",
            ContactEmail = "testuser@test.com",
            PostalCode = "12-345"
        };

        var validator = new CreateRestaurantDtoValidator();

        //act
        var result = validator.TestValidate(restaurant);

        //assess

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]

    public void Validator_ForInvalidRestaurant_ReturnValidationErrors()
    {
        var restaurant = new CreateRestaurantCommand()
        {
            Name = "Ts",
            Category = "pakistani",
            ContactEmail = "test.com",
            PostalCode = "11-233"
        };

        var validator = new CreateRestaurantDtoValidator();

        //acr
        var result = validator.TestValidate(restaurant);

        //assess
        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        result.ShouldNotHaveValidationErrorFor(c => c.PostalCode);
    }

    [Theory()]
    [InlineData("Indian")]
    [InlineData("Mexican")]
    [InlineData("Italian")]
    [InlineData("Chinese")]
    public void Validator_ForValidCategory_ReturnNoValidationErrors(string category)
    {
        //arrange
        var command = new CreateRestaurantCommand() { Category = category };

        var validator = new CreateRestaurantDtoValidator();

        //act
        var result = validator.TestValidate(command);

        //assess
        result.ShouldNotHaveValidationErrorFor(c => c.Category);
    }

    [Theory()]
    [InlineData("11")]
    [InlineData("11-22")]
    [InlineData("111-333")]
    [InlineData("111-33")]
    public void Validator_ForInvalidPostalCodes_ReturnValidationErrors(string postalCode)
    {
        var command = new CreateRestaurantCommand() { PostalCode = postalCode };
        var validator = new CreateRestaurantDtoValidator();

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }
}