namespace BurgerBackend.Api.Contracts.Models;

public class Review
{
    public Guid ReviewId { get; set; } = Guid.NewGuid();

    public string ReviewerId { get; set; } = string.Empty;

    public IEnumerable<Scoring> Scorings { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
}