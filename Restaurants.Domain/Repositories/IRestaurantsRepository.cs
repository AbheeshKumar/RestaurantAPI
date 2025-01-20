using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;
public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetSpecificAsync(int RestaurantId);
    Task<int> Create(Restaurant restaurant);
    Task Delete(Restaurant restaurant);

    Task SaveChanges();
}
