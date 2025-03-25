using FluentValidation;

namespace Restaurants.Application.Restaurants.Query.GetAllRestaurants;

public class GetAllRestaurantsValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private readonly int[] _allowedPageSizes = [5,10,15,30];
    public GetAllRestaurantsValidator()
    {
        RuleFor(r => r.pageNumber)
            .GreaterThan(0);

        RuleFor(r => r.pageSize)
            .Must(Value => _allowedPageSizes.Contains(Value))
            .WithMessage($"Page size must be in [{String.Join(",", _allowedPageSizes)}]");
    }
}
