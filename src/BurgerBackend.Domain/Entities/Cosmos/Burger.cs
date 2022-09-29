using Newtonsoft.Json;

namespace BurgerBackend.Domain.Entities.Cosmos;

public class Burger
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "price")]
    public double Price { get; set; }
}