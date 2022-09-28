using Newtonsoft.Json;

namespace BurgerBackend.Domain.Entities.Cosmos;

public class Entity
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [JsonProperty(PropertyName = "pk")]
    public string PartitionKey { get; set; } = string.Empty;
}