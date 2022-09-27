using System.Text.Json.Serialization;

namespace BurgerBackend.Domain.Entities.Cosmos;

public class Review
{
    [JsonPropertyName("reviewId")] 
    public Guid Id { get; set; } = Guid.NewGuid();

    [JsonPropertyName("reviewerId")]
    public string ReviewerId { get; set; }

    [JsonPropertyName("scorings")]
    public IEnumerable<Scoring> Scorings { get; set; }

    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; } = string.Empty;
}