using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Domain.Constants;
using Xunit;

namespace Restaurants.Application.Users.Tests;

public class UserContextTests
{
    [Fact()]
    public void GetCurrentUser_AuthenticatedUser_ReturnUser()
    {
        //arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        var userClaims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, "1"),
            new(ClaimTypes.Email, "testuser@test.com"),
            new(ClaimTypes.Role, UserRoles.admin),
            new(ClaimTypes.Role, UserRoles.user),
            new("Nationality", "Pakistani"),
            new("DateOfBirth", new DateOnly(2001, 8, 14).ToString("yyyy-MM-dd"))
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(userClaims, "Test"));

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });

        var userContext = new UserContext(httpContextAccessorMock.Object);

        //act
        var currentUser = userContext.GetCurrentUser();

        //assert
        currentUser.Should().NotBeNull();
        currentUser.Id.Should().Be("1");
        currentUser.Email.Should().Be("testuser@test.com");
        currentUser.Roles.Should().Contain(UserRoles.admin, UserRoles.user);
        currentUser.Nationality.Should().Be("Pakistani");
        currentUser.DateOfBirth.Should().Be(new DateOnly(2001, 8, 14));
    }

    [Fact()]
    public void GetCurrentUser_NullUserContext_ThrowException()
    {
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

        var user = new UserContext(httpContextAccessorMock.Object);

        Action action = () => user.GetCurrentUser();

        action.Should().Throw<InvalidOperationException>()
            .WithMessage("User Context is not present");
    }
}