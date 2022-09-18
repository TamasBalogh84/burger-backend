namespace BurgerBackend.Domain.Entities.Cosmos
{
    public class Review
    {
        public string ReviewerId { get; set; }

        public IEnumerable<Scoring> Scoring { get; set; }

        public string Picture { get; set; }
    }
}
