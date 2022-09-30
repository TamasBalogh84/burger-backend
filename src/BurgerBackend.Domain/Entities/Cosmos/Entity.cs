using Newtonsoft.Json;

namespace BurgerBackend.Domain.Entities.Cosmos;

public record Entity
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; init; } = Guid.NewGuid().ToString();

    [JsonProperty(PropertyName = "pk")]
    public string PartitionKey { get; set; } = string.Empty;
}