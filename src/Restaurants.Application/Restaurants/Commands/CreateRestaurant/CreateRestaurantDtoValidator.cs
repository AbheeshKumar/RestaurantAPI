using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;
public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string?> _categories = ["Italian", "Indian", "Chinese", "Mexican"];
    public CreateRestaurantDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Category)
            //.Must(category => _categories.Contains(category))
            .Must(_categories.Contains)
            .WithMessage("Invalid Category");
        //.Custom((value, context) =>
        //{
        //    bool isValidCategory = _categories.Contains(value);
        //    if (!isValidCategory) {
        //        context.AddFailure("Invalid Category");
        //    }
        //});

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Enter a valid Emai Address");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide in this format XX-XXX");
    }
}
