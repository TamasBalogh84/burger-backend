using Newtonsoft.Json;

namespace BurgerBackend.Domain.Entities.Cosmos;

public class OpeningTime
{
    [JsonProperty(PropertyName = "day")]
    public string Day { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "openingStartTime")]
    public string OpeningStartTime { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "openingEndTime")]
    public string OpeningEndTime { get; set; } = string.Empty;
}