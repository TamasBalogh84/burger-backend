using System.Text.Json.Serialization;

namespace BurgerBackend.Domain.Entities.Cosmos
{
    public class Scoring
    {
        [JsonPropertyName("scoringName")]
        public string ScoringName { get; set; } = string.Empty;

        [JsonPropertyName("scoringValue")]
        public decimal ScoreValue { get; set; }
    }
}
