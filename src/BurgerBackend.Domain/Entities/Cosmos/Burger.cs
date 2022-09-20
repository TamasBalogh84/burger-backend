using System.Text.Json.Serialization;

namespace BurgerBackend.Domain.Entities.Cosmos
{
    public class Burger
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
    }
}
