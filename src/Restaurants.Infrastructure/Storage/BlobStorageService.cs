using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Configurations;

namespace Restaurants.Infrastructure.Storage;

internal class BlobStorageService(IOptions<BlobStorageSettings> BlobStorageOptions) : IBlobStorageService
{
    private readonly BlobStorageSettings _BlobStorageSettings = BlobStorageOptions.Value;
    public async Task<string> UploadToBlobAsync(Stream Data, string Filename)
    {
        //Connect with Blob
        var blobServiceClient = new BlobServiceClient(_BlobStorageSettings.ConnectionString);
        //Get the container
        var containerClient = blobServiceClient.GetBlobContainerClient(_BlobStorageSettings.LogoContainerName);

        //Create blob
        var blobClient = containerClient.GetBlobClient(Filename);

        //Upload
        await blobClient.UploadAsync(Data);

        var blobUrl = blobClient.Uri.ToString();

        return blobUrl;

    }

    public string? GetBlobUrl(string? blobUrl)
    {

        if (blobUrl == null) return null;

        var sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = _BlobStorageSettings.LogoContainerName,
            BlobName = GetBlogName(blobUrl),
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30),
            Resource = "b"
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        var blobClient = new BlobServiceClient(_BlobStorageSettings.ConnectionString);

        var sharedKey = new StorageSharedKeyCredential(blobClient.AccountName, _BlobStorageSettings.AccountKey);

        var sasToken = sasBuilder.ToSasQueryParameters(sharedKey).ToString();

        return $"{blobUrl}?{sasToken}";
    }
    public string GetBlogName(string blobUrl)
    {
        var uri = new Uri(blobUrl);
        return uri.Segments.Last();
    }
}
