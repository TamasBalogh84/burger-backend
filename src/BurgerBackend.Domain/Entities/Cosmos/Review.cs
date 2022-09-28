using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace BurgerBackend.Domain.Entities.Cosmos;

public class Review
{
    [JsonProperty(PropertyName = "reviewId")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [JsonPropertyName("reviewerId")]
    public string ReviewerId { get; set; }

    [JsonPropertyName("scorings")]
    public IEnumerable<Scoring> Scorings { get; set; }

    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; } = string.Empty;
}