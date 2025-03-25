using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Query.GetAllRestaurants;

public class GetAllRestaurantsValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private readonly int[] _allowedPageSizes = [5,10,15,30];
    private readonly string[] _allowedColumnNames =
        [nameof(RestaurantDto.Name), nameof(RestaurantDto.Category), nameof(RestaurantDto.Description)];

    public GetAllRestaurantsValidator()
    {
        RuleFor(r => r.pageNumber)
            .GreaterThan(0);

        RuleFor(r => r.pageSize)
            .Must(Value => _allowedPageSizes.Contains(Value))
            .WithMessage($"Page size must be in [{String.Join(",", _allowedPageSizes)}]");

        RuleFor(r => r.sortBy)
            .Must(c => _allowedColumnNames.Contains(c))
            .When(q => q.sortBy!=null)
            .WithMessage($"Category should be in [{string.Join(',', _allowedColumnNames)}]");
    }
}
