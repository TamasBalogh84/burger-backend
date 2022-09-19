﻿namespace BurgerBackend.Domain.Entities.Cosmos
{
    public class Review
    {
        public Guid Id { get; set; }

        public Guid ReviewerId { get; set; }

        public IEnumerable<Scoring> Scorings { get; set; }

        public string Picture { get; set; }
    }
}
