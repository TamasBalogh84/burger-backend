namespace BurgerBackend.Api.Contracts.Models
{
    public class ReviewRequest
    {
        public string ReviewerId { get; init; } = string.Empty;

        public IEnumerable<Scoring> Scorings { get; init; }

        public string ImageUrl { get; init; } = string.Empty;
    }
}
