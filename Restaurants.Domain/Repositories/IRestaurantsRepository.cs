using Restaurants.Application.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;
public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    public Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchParam, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    Task<Restaurant?> GetSpecificAsync(int RestaurantId);
    Task<int> Create(Restaurant restaurant);
    Task Delete(Restaurant restaurant);

    Task SaveChanges();
}
