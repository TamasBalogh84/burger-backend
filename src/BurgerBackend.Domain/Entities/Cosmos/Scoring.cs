using Newtonsoft.Json;

namespace BurgerBackend.Domain.Entities.Cosmos;

public record Scoring([JsonProperty(PropertyName = "scoringName")] string ScoringName, [JsonProperty(PropertyName = "scoringValue")] double ScoreValue);