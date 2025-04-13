using FluentAssertions;
using Restaurants.Domain.Constants;
using Xunit;

namespace Restaurants.Application.Users.Tests;

public class CurrentUserTests
{
    [Theory()]
    [InlineData(UserRoles.admin)]
    [InlineData(UserRoles.user)]
    
    //Test Naming: Mehtod_Scenario_Return
    public void IsRole_WithMatchingRole_ReturnTrue(string roleName)
    {
        //arrange
        var test_user = new CurrentUser("1", "testuser@test.com", [UserRoles.admin, UserRoles.user], null, null, null);

        //act
        var isRole = test_user.IsRole(roleName);

        //assert
        isRole.Should().BeTrue();
    }

    [Fact()]
    public void IsRole_WithNoMatchingRole_ReturnFalse()
    {
        var test_user = new CurrentUser("1", "testuser@test.com", [UserRoles.admin, UserRoles.user], null, null, null);

        var isRole = test_user.IsRole(UserRoles.owner);

        isRole.Should().BeFalse();
    }


}