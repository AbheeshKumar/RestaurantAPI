using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.CreateDishes;

public class CreateDishesValidator : AbstractValidator<CreateDishesCommand>
{
    public CreateDishesValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage("Name is necessary")
            .MaximumLength(30).WithMessage("Name should be less than 100 characters");

        RuleFor(d => d.KiloCalories)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Calories Should be greater than zero");

        RuleFor(d => d.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price should be non negative number");
    }
}
