
using AutoMapper;
using Restaurants.Application.Dishes.Commands.CreateDishes;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Dtos;
public class DishesProfile : Profile
{
    public DishesProfile()
    {
        CreateMap<CreateDishesCommand, Dish>();
        CreateMap<Dish, DishesDto>();
    }
}
