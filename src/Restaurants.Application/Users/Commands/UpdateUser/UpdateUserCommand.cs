using MediatR;

namespace Restaurants.Application.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest
{
    public DateOnly? DateofBirth { get; set; }
    public string? Nationality { get; set; }
}
