using System.Text.Json.Serialization;

namespace BurgerBackend.Domain.Entities.Cosmos
{
    public class Entity
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("pk")]
        public string PartitionKey { get; set; } = string.Empty;
    }
}
