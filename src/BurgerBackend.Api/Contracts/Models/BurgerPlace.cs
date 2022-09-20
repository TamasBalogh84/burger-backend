namespace BurgerBackend.Api.Contracts.Models
{
    public class BurgerPlace
    {
        public Guid Id { get; set; }

        public IEnumerable<Burger> AvailableBurgers { get; set; }

        public string Information { get; set; } = string.Empty;

        public Location Location { get; set; }

        public string OpeningTime { get; set; } = string.Empty;

        public IEnumerable<Review> Reviews { get; set; }
    }
}
