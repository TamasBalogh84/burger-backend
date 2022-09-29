using Newtonsoft.Json;

namespace BurgerBackend.Domain.Entities.Cosmos;

public class Review
{
    [JsonProperty(PropertyName = "reviewId")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [JsonProperty(PropertyName = "reviewerId")]
    public string ReviewerId { get; set; }

    [JsonProperty(PropertyName = "scorings")]
    public IEnumerable<Scoring> Scorings { get; set; }

    [JsonProperty(PropertyName = "imageUrl")]
    public string ImageUrl { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "createdDate")]
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
}