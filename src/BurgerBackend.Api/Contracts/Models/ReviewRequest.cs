namespace BurgerBackend.Api.Contracts.Models;

public record ReviewRequest(string ReviewText, IEnumerable<Scoring> Scorings, string ImageUrl);