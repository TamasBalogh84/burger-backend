﻿namespace BurgerBackend.Domain.Entities.Cosmos
{
    public class BurgerPlace : Entity
    {
        public IEnumerable<Burger> AvailableBurgers { get; set; }

        public Location Location { get; set; }

        public string OpeningTime { get; set; } = string.Empty;

        public IEnumerable<Review> Reviews { get; set; }
    }
}