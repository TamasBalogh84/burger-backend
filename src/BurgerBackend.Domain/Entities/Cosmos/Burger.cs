using Newtonsoft.Json;

namespace BurgerBackend.Domain.Entities.Cosmos;

public record Burger([JsonProperty(PropertyName = "name")] string Name, [JsonProperty(PropertyName = "price")] double Price);