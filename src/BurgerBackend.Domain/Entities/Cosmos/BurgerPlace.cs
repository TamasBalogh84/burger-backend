using System.Text.Json.Serialization;

namespace BurgerBackend.Domain.Entities.Cosmos;

public class BurgerPlace : Entity
{
    [JsonPropertyName("availableBurgers")]
    public IEnumerable<Burger> AvailableBurgers { get; set; }

    [JsonPropertyName("information")]
    public string Information { get; set; } = string.Empty;

    [JsonPropertyName("location")]
    public Location Location { get; set; }

    [JsonPropertyName("openingTime")]
    public string OpeningTime { get; set; } = string.Empty;

    [JsonPropertyName("reviews")]
    public IEnumerable<Review> Reviews { get; set; }
}