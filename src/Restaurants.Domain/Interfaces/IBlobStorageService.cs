
namespace Restaurants.Domain.Interfaces;

public interface IBlobStorageService
{
    string? GetBlobUrl(string? blobUrl);
    Task<string> UploadToBlobAsync(Stream Data, string Filename);
}
