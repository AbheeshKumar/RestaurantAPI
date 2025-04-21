namespace Restaurants.Infrastructure.Configurations;

public class BlobStorageSettings
{
    public string ConnectionString { get; set; } = default!;
    public string LogoContainerName { get; set; } = default!;
    public string AccountName { get; set; } = default!;
    public string AccountKey { get; set; } = default!;
}
