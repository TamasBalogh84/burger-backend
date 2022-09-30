using Newtonsoft.Json;

namespace BurgerBackend.Domain.Entities.Cosmos;

public record Review
{
    [JsonProperty(PropertyName = "reviewId")]
    public string Id { get; init; } = Guid.NewGuid().ToString();

    [JsonProperty(PropertyName = "reviewerId")]
    public string ReviewerId { get; init; } = string.Empty;

    [JsonProperty(PropertyName = "reviewText")]
    public string ReviewText { get; init; } = string.Empty;

    [JsonProperty(PropertyName = "scorings")]
    public IEnumerable<Scoring> Scorings { get; init; }

    [JsonProperty(PropertyName = "imageUrl")]
    public string ImageUrl { get; init; } = string.Empty;

    [JsonProperty(PropertyName = "createdDate")]
    public DateTimeOffset CreatedDate { get; init; } = DateTimeOffset.UtcNow;
}