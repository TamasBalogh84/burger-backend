namespace BurgerBackend.Domain.Entities.Cosmos
{
    public class BurgerPlace : Entity
    {
        public IEnumerable<Burger> AvailableBurgers { get; set; }

        public Location Location { get; set; }

        public string OpeningTime { get; set; }

        public IEnumerable<Review> Reviews { get; set; }
    }
}
