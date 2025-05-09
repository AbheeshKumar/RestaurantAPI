﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Seeders;
internal class RestaurantSeeders(RestaurantDbContext dbContext) : IRestaurantSeeders
{
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }

        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Restuarants.Any())
            {
                var restaurants = GetRestaurants();
                await dbContext.Restuarants.AddRangeAsync(restaurants);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetUserRoles();
                await dbContext.AddRangeAsync(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private static IEnumerable<IdentityRole> GetUserRoles()
    {
        List<IdentityRole> userRoles = [
            new (UserRoles.user)
            {
                NormalizedName = UserRoles.user.ToUpper()
            },
            new (UserRoles.admin){
                NormalizedName = UserRoles.admin.ToUpper()
            },
            new (UserRoles.owner){
                NormalizedName = UserRoles.owner.ToUpper()
            }
        ];

        return userRoles;
    }

    private static List<Restaurant> GetRestaurants()
    {
        User user = new()
        {
            Email = "seed-user@test.com"
        };

        List<Restaurant> restaurants = [
            new()
            {
                Name = "KFC",
                Owner = user,
                Category = "Fast Food",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes =
                [
                    new ()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M,
                    },

                    new ()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M,
                    },
                ],
                Address = new ()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                }
            },
            new ()
            {
                Name = "McDonald",
                Owner = user,
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Dishes = [
                    new() {
                        Name = "Chicken Spicy Burger",
                        Description = "Chicken Spicy Burger (1 pc)",
                        Price = 6.50M
                    }
                ],
                Address = new Address()
                {
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F 8SR"
                }
            }
        ];
        return restaurants;
    }
}

