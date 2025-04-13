namespace Restaurants.Infrastructure.Authorization;

public class PolicyNames
{
    public const string HasNationality = "HasNationality";
    public const string Atleast20 = "Atleast20";
    public const string Atleast2 = "Atleast2";
}

public class ClaimTypes
{
    public const string Nationality = "Nationality";
    public const string DateOfBirth = "DateOfBirth";
    public const string OwnedRestaurants = "OwnedRestaurants";
}