using Newtonsoft.Json;

namespace BurgerBackend.Domain.Entities.Cosmos;

public record Burger([JsonProperty(PropertyName = "name")] string Name, [JsonProperty(PropertyName = "price")] double Price);
//{
//    [JsonProperty(PropertyName = "name")]
//    public string Name { get; set; } = string.Empty;

//    [JsonProperty(PropertyName = "price")]
//    public double Price { get; set; }
//}