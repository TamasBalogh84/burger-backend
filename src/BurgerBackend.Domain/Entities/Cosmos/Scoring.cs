using Newtonsoft.Json;

namespace BurgerBackend.Domain.Entities.Cosmos;

public class Scoring
{
    [JsonProperty(PropertyName = "scoringName")]
    public string ScoringName { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "scoringValue")]
    public double ScoreValue { get; set; }
}