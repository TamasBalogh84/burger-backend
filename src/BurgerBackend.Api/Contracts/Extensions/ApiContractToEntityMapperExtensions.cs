using BurgerBackend.Domain.Entities.Cosmos;

namespace BurgerBackend.Api.Contracts.Extensions;

public static class ApiContractToEntityMapperExtensions
{
    public static Review ToReview(this Models.Review review)
    {
        return new Review
        {
            Id = review.ReviewId,
            ReviewerId = review.ReviewerId,
            Scorings = review.Scorings.Select(s => s.ToScoring()),
            ImageUrl = review.ImageUrl
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

    public static BurgerPlace ToBurgerPlace(this Models.BurgerPlace place)
    {
        return new BurgerPlace()
        {
            AvailableBurgers = place.AvailableBurgers.ToBurgers(),
            Information = place.Information,
            Location = place.Location.ToLocation(),
            OpeningTime = place.OpeningTime,
            Reviews = place.Reviews.Select(r => r.ToReview())
        };
    }

    public static IEnumerable<Burger> ToBurgers(this IEnumerable<Models.Burger> burgers)
    {
        return burgers.Select(b => new Burger()
        {
            Name = b.Name,
            Price = b.Price
        });
    }

    public static Location ToLocation(this Models.Location location)
    {
        return new Location()
        {
            City = location.City,
            Address = location.Address,
            Coordinates = location.Coordinates
        };
    }
}