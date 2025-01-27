using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantRepository(RestaurantDbContext dbContext) : IRestaurantsRepository
    {
        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await dbContext.Restuarants.Include(r=>r.Dishes).ToListAsync();

            return restaurants;
        }

        public async Task<Restaurant?> GetSpecificAsync(int RestaurantId)
        {
            var restaurant = await dbContext.Restuarants.
                Include(r => r.Dishes).
                FirstOrDefaultAsync(x => x.Id == RestaurantId);

            return restaurant;
        }

        public async Task<int> Create(Restaurant restaurant)
        {
            await dbContext.Restuarants.AddAsync(restaurant);
            await dbContext.SaveChangesAsync();

            return restaurant.Id;
        }

        public async Task Delete(Restaurant restaurant)
        {
            dbContext.Remove(restaurant);
            await dbContext.SaveChangesAsync();
        }

        public async Task SaveChanges()
        => await dbContext.SaveChangesAsync();
    }
}
