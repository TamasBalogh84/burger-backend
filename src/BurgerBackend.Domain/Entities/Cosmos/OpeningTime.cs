using Newtonsoft.Json;

namespace BurgerBackend.Domain.Entities.Cosmos;

public record OpeningTime(
    [JsonProperty(PropertyName = "day")] string Day, 
    [JsonProperty(PropertyName = "openingStartTime")] string OpeningStartTime, 
    [JsonProperty(PropertyName = "openingEndTime")] string OpeningEndTime);