namespace BurgerBackend.Api.Contracts.Models
{
    public class Review
    {
        public Guid Id { get; set; }

        public Guid ReviewerId { get; set; }

        public IEnumerable<Scoring> Scorings { get; set; }

        public string Picture { get; set; }
    }
}
