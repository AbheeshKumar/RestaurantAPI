using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IDishRepository
{
    Task<int> Create(Dish dish);
    Task DeleteSpecific(Dish dish);

    Task DeleteAll(IEnumerable<Dish> dishes);
}
