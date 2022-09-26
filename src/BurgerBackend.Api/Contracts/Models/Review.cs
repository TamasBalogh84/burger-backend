namespace BurgerBackend.Api.Contracts.Models;

public class Review
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string ReviewerId { get; set; }

    public IEnumerable<Scoring> Scorings { get; set; }

    public string ImageUrl { get; set; }
}