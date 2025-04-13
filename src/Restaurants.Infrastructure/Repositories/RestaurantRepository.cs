using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantRepository(RestaurantDbContext dbContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restuarants.Include(r=>r.Dishes).ToListAsync();

        return restaurants;
    }

    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(
        string? searchParam, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
    {
        var searchParamLower = searchParam?.ToLower();

        var baseQuery = dbContext.Restuarants
            .Include(r => r.Dishes)
            .Where(r => searchParamLower == null || (
                    r.Name.Contains(searchParamLower) || r.Description.Contains(searchParamLower)) 
                 );

        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null)
        {
            var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                {nameof(Restaurant.Name), r=>r.Name },
                {nameof(Restaurant.Description), r=>r.Description },
                {nameof(Restaurant.Category), r=>r.Category }
            };
            var selectedColumn = columnSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Asc ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }
        var restaurants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (restaurants, totalCount);

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
