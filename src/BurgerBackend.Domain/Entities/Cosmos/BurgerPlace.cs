using Newtonsoft.Json;

namespace BurgerBackend.Domain.Entities.Cosmos;

public record BurgerPlace : Entity
{
    [JsonProperty(PropertyName = "availableBurgers")]
    public IEnumerable<Burger> AvailableBurgers { get; set; }

    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "information")]
    public string Information { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "location")]
    public Location Location { get; set; }

    [JsonProperty(PropertyName = "openingTimes")]
    public IEnumerable<OpeningTime> OpeningTimes { get; set; }

    [JsonProperty(PropertyName = "reviews")]
    public IEnumerable<Review> Reviews { get; set; }
}