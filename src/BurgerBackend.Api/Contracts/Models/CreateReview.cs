namespace BurgerBackend.Api.Contracts.Models;

public record CreateReview
{
    public string ReviewerId { get; init; } = string.Empty;

    public string ReviewText { get; init; } = string.Empty;

    public IEnumerable<Scoring> Scorings { get; init; }

    public string ImageUrl { get; init; } = string.Empty;
}