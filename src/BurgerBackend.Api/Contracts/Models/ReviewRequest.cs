namespace BurgerBackend.Api.Contracts.Models
{
    public class ReviewRequest
    {
        public string ReviewerId { get; set; }

        public IEnumerable<Scoring> Scorings { get; set; }

        public string ImageUrl { get; set; }
    }
}
