using Newtonsoft.Json;

namespace BurgerBackend.Domain.Entities.Cosmos;

public class Location
{
    [JsonProperty(PropertyName = "city")]
    public string City { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "address")]
    public string Address { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "coordinates")]
    public string Coordinates { get; set; } = string.Empty;
}