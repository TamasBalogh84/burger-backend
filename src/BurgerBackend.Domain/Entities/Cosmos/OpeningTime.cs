using Newtonsoft.Json;

namespace BurgerBackend.Domain.Entities.Cosmos;

public record OpeningTime(
    [JsonProperty(PropertyName = "day")] string Day, 
    [JsonProperty(PropertyName = "openingStartTime")] string OpeningStartTime, 
    [JsonProperty(PropertyName = "openingEndTime")] string OpeningEndTime);
//{
//    [JsonProperty(PropertyName = "day")]
//    public string Day { get; set; } = string.Empty;

//    [JsonProperty(PropertyName = "openingStartTime")]
//    public string OpeningStartTime { get; set; } = string.Empty;

//    [JsonProperty(PropertyName = "openingEndTime")]
//    public string OpeningEndTime { get; set; } = string.Empty;
//}