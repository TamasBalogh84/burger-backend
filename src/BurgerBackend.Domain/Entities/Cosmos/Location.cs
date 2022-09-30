using Newtonsoft.Json;

namespace BurgerBackend.Domain.Entities.Cosmos;

public record Location(
    [JsonProperty(PropertyName = "city")] string City, 
    [JsonProperty(PropertyName = "address")] string Address, 
    [JsonProperty(PropertyName = "coordinates")] string Coordinates);
//{
//    [JsonProperty(PropertyName = "city")]
//    public string City { get; set; } = string.Empty;

//    [JsonProperty(PropertyName = "address")]
//    public string Address { get; set; } = string.Empty;

//    [JsonProperty(PropertyName = "coordinates")]
//    public string Coordinates { get; set; } = string.Empty;
//}