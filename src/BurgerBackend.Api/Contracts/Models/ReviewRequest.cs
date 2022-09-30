namespace BurgerBackend.Api.Contracts.Models;

public record ReviewRequest(string ReviewText, IEnumerable<Scoring> Scorings, string ImageUrl);

//{
//public string ReviewerId { get; init; } = string.Empty;

//public IEnumerable<Scoring> Scorings { get; init; }

//public string ImageUrl { get; init; } = string.Empty;
//}