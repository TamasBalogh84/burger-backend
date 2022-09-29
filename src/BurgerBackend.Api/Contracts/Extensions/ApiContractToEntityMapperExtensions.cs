using BurgerBackend.Domain.Entities.Cosmos;

namespace BurgerBackend.Api.Contracts.Extensions;

public static class ApiContractToEntityMapperExtensions
{
    public static Review ToReview(this Models.Review review)
    {
        return new Review
        {
            Id = review.ReviewId.ToString(),
            ReviewerId = review.ReviewerId,
            Scorings = review.Scorings.Select(s => s.ToScoring()),
            ImageUrl = review.ImageUrl,
            CreatedDate = review.CreatedDate
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
        return new BurgerPlace
        {
            Id = place.Id.ToString(),
            AvailableBurgers = place.AvailableBurgers.ToBurgers(),
            Name = place.Name,
            Information = place.Information,
            Location = place.Location.ToLocation(),
            OpeningTimes = place.OpeningTimes.Select(o => o.ToOpeningTime()),
            Reviews = place.Reviews.Select(r => r.ToReview())
        };
    }

    private static IEnumerable<Burger> ToBurgers(this IEnumerable<Models.Burger> burgers)
    {
        return burgers.Select(b => new Burger
        {
            Name = b.Name,
            Price = b.Price
        });
    }

    private static Location ToLocation(this Models.Location location)
    {
        return new Location
        {
            City = location.City,
            Address = location.Address,
            Coordinates = location.Coordinates
        };
    }

    private static OpeningTime ToOpeningTime(this Models.OpeningTime openingtime)
    {
        return new OpeningTime
        {
            Day = openingtime.Day,
            OpeningStartTime = openingtime.OpeningStartTime,
            OpeningEndTime = openingtime.OpeningEndTime
        };
    }
}