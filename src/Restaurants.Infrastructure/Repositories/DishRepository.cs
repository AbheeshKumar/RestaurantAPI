using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Repositories;

internal class DishRepository(RestaurantDbContext dbContext) : IDishRepository
{
    public async Task<int> Create(Dish dish)
    {
        await dbContext.Dishes.AddAsync(dish);
        await dbContext.SaveChangesAsync();
        return dish.Id;
    }

    public async Task DeleteSpecific(Dish dish)
    {
        dbContext.Remove(dish);
        await dbContext.SaveChangesAsync();
    }
    public async Task DeleteAll(IEnumerable<Dish> dishes)
    {
        dbContext.RemoveRange(dishes);
        await dbContext.SaveChangesAsync();
    }

}
