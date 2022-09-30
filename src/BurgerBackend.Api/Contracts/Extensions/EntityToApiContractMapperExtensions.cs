using BurgerBackend.Api.Contracts.Models;

namespace BurgerBackend.Api.Contracts.Extensions;

public static class EntityToApiContractMapperExtensions
{

    public static BurgerPlace ToBurgerPlace(this Domain.Entities.Cosmos.BurgerPlace burgerPlace)
    {
        if (burgerPlace is null)
        {
            throw new ArgumentNullException(nameof(burgerPlace));
        }

        return new BurgerPlace
        {
            Id = Guid.Parse(burgerPlace.Id),
            AvailableBurgers = ToBurgers(burgerPlace.AvailableBurgers),
            Name = burgerPlace.Name,
            Information = burgerPlace.Information,
            OpeningTimes = burgerPlace.OpeningTimes.Select(o => o.ToOpeningTime()),
            Location = ToLocation(burgerPlace.Location),
            Reviews = burgerPlace.Reviews.Select(r => r.ToReview())
        };
    }

    public static Review ToReview(this Domain.Entities.Cosmos.Review review)
    {
        if (review is null)
        {
            throw new ArgumentNullException(nameof(review));
        }

        return new Review
        {
            ReviewId = Guid.Parse(review.Id),
            ReviewerId = review.ReviewerId,
            ReviewText = review.ReviewText,
            Scorings = review.Scorings.Select(s => s.ToScoring()),
            ImageUrl = review.ImageUrl,
            CreatedDate = review.CreatedDate
        };
    }

    private static IEnumerable<Burger> ToBurgers(this IEnumerable<Domain.Entities.Cosmos.Burger> burgers)
    {
        if (burgers is null)
        {
            throw new ArgumentNullException(nameof(burgers));
        }

        return burgers
            .Select(b => new Burger (Name: b.Name, Price: b.Price))
            .ToList();
    }

    private static OpeningTime ToOpeningTime(this Domain.Entities.Cosmos.OpeningTime openingTime)
    {
        return new OpeningTime(Day: openingTime.Day, OpeningStartTime: openingTime.OpeningStartTime,
            OpeningEndTime: openingTime.OpeningEndTime);
    }

    private static Location ToLocation(this Domain.Entities.Cosmos.Location location)
    {
        if (location is null)
        {
            throw new ArgumentNullException(nameof(location));
        }

        return new Location(City: location.City, Address: location.Address, Coordinates: location.Coordinates);
    }

    private static Scoring ToScoring(this Domain.Entities.Cosmos.Scoring scoring)
    {
        if (scoring is null)
        {
            throw new ArgumentNullException(nameof(scoring));
        }

        return new Scoring(ScoringName: scoring.ScoringName, ScoreValue: scoring.ScoreValue);
    }
}