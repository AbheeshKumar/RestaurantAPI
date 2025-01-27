using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;
namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(r => r.Address, opt => opt.MapFrom(src => new Address
            {
                City = src.City,
                PostalCode = src.PostalCode,
                Street = src.Street,
            }));

        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(r => r.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(r => r.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
            .ForMember(r => r.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
            .ForMember(r => r.Dishes, opt => opt.MapFrom(src => src == null ? null : src.Dishes));

        CreateMap<UpdateRestaurantCommand, Restaurant>();
    }
}