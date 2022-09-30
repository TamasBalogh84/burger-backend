using Newtonsoft.Json;

namespace BurgerBackend.Domain.Entities.Cosmos;

public record Location(
    [JsonProperty(PropertyName = "city")] string City, 
    [JsonProperty(PropertyName = "address")] string Address, 
    [JsonProperty(PropertyName = "coordinates")] string Coordinates);