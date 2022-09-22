using System.Text.Json.Serialization;

namespace BurgerBackend.Domain.Entities.Cosmos;

public class Location
{
    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("coordinates")]
    public string Coordinates { get; set; } = string.Empty;
}