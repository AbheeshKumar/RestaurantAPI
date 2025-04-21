using MediatR;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.Restaurants.Commands.UploadFile;

public class UploadRestaurantLogoCommand() : IRequest
{
    public int RestaurantId { get; set; }
    public string FileName { get; set; } = default!;
    public Stream File { get; set; } = default!;
}
