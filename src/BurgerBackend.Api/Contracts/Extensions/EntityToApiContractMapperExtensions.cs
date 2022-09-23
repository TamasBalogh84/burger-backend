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
            Id = burgerPlace.Id,
            AvailableBurgers = ToBurgers(burgerPlace.AvailableBurgers),
            Information = burgerPlace.Information,
            OpeningTime = burgerPlace.OpeningTime,
            Location = ToLocation(burgerPlace.Location),
            Reviews = ToReviews(burgerPlace.Reviews)
        };
    }

    public static IEnumerable<BurgerPlace> ToBurgerPlaces(this IEnumerable<Domain.Entities.Cosmos.BurgerPlace> burgerPlaces)
    {
        if (burgerPlaces is null)
        {
            throw new ArgumentNullException(nameof(burgerPlaces));
        }

        return burgerPlaces.Select(p => p.ToBurgerPlace());
    }

    public static IEnumerable<Burger> ToBurgers(this IEnumerable<Domain.Entities.Cosmos.Burger> burgers)
    {
        if (burgers is null)
        {
            throw new ArgumentNullException(nameof(burgers));
        }

        return burgers
            .Select(b => new Burger
            {
                Name = b.Name,
                Price = b.Price
            })
            .ToList();
    }

    public static Location ToLocation(this Domain.Entities.Cosmos.Location location)
    {
        if (location is null)
        {
            throw new ArgumentNullException(nameof(location));
        }

        return new Location
        {
            Address = location.Address, 
            City = location.City, 
            Coordinates = location.Coordinates
        };
    }

    public static IEnumerable<Review> ToReviews(this IEnumerable<Domain.Entities.Cosmos.Review> reviews)
    {
        if (reviews is null)
        {
            throw new ArgumentNullException(nameof(reviews));
        }

        return reviews
            .Select(r => r.ToReview())
            .ToList();
    }

    public static Review ToReview(this Domain.Entities.Cosmos.Review review)
    {
        if (review is null)
        {
            throw new ArgumentNullException(nameof(review));
        }

        return new Review
        {
            Id = review.Id,
            ReviewerId = review.ReviewerId,
            Scorings = review.Scorings.ToScorings(),
            ImageUrl = review.ImageUrl
        };
    }

    public static IEnumerable<Scoring> ToScorings(this IEnumerable<Domain.Entities.Cosmos.Scoring> scorings)
    {
        if (scorings is null)
        {
            throw new ArgumentNullException(nameof(scorings));
        }

        return scorings.Select(s => new Scoring
        {
            ScoringName = s.ScoringName,
            ScoreValue = s.ScoreValue
        });
    }
}