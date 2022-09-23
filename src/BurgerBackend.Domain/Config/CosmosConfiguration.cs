namespace BurgerBackend.Domain.Config;

public class CosmosConfiguration
{
    public string ConnectionString { get; set; } = string.Empty;

    public string Database { get; set; } = string.Empty;

    public string BurgerPlacesContainerName { get; set; } = string.Empty;

    public string BurgerPlacesPartitionKey { get; set; } = string.Empty;
}