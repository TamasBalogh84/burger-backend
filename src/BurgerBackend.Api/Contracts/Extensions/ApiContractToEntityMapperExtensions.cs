using BurgerBackend.Domain.Entities.Cosmos;

namespace BurgerBackend.Api.Contracts.Extensions
{
    public static class ApiContractToEntityMapperExtensions
    {
        public static Review ToReview(this Models.Review review)
        {
            return new Review
            {
                Id = new Guid(),
                ReviewerId = review.ReviewerId,
                Scorings = review.Scorings.Select(s => s.ToScoring()),
                Picture = review.Picture
            };
        }

        public static Scoring ToScoring(this Models.Scoring scoring)
        {
            return new Scoring
            {
                ScoringName = scoring.ScoringName,
                ScoreValue = scoring.ScoreValue
            };
        }
    }
}
