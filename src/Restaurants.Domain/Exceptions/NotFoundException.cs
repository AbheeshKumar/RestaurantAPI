
namespace Restaurants.Domain.Exceptions;

public class NotFoundException(string resourceType, string resourceIdentifyer) 
                : Exception($"{resourceType} with Id: {resourceIdentifyer} doesn't exist")
{
}
