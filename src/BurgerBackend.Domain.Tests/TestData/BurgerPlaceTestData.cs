using System;
using System.Collections.Generic;
using BurgerBackend.Domain.Entities.Cosmos;

namespace BurgerBackend.Api.Tests.TestData;

public record BurgerPlaceTestData (
    Guid PlaceId,
    IEnumerable<Burger> AvailableBurgers,
    string Information,
    Location Location,
    string OpeningTime,
    IEnumerable<Review> Reviews)
{
    public readonly static BurgerPlaceTestData ContainingOneReview = new(
        PlaceId: Guid.NewGuid(),
        AvailableBurgers: new List<Burger>()
        {
            new ()
            {
                Name = "test burger",
                Price = 10.0
            }
        },
        Information: "this is a good place",
        Location: new Location()
        {
            City = "test city",
            Address = "test str 12",
            Coordinates = "10,32.46 65,65.43"
        },
        OpeningTime: "0-24",
        Reviews: new List<Review>()
        {
            new ()
            {
                Id = Guid.NewGuid(),
                ReviewerId = "test@test.com",
                ImageUrl = "abc.test.com/abc.png",
                Scorings = new List<Scoring>()
                {
                    new ()
                    {
                        ScoringName = "texture",
                        ScoreValue = 4.4
                    }
                }
            }
        });

    public readonly static BurgerPlaceTestData ContainingMoreReviews = new(
        PlaceId: Guid.NewGuid(),
        AvailableBurgers: new List<Burger>()
        {
            new ()
            {
                Name = "test burger",
                Price = 10.0
            }
        },
        Information: "this is a good place",
        Location: new Location()
        {
            City = "test city",
            Address = "test str 12",
            Coordinates = "10,32.46 65,65.43"
        },
        OpeningTime: "0-24",
        Reviews: new List<Review>()
        {
            new ()
            {
                Id = Guid.NewGuid(),
                ReviewerId = "test2@test.com",
                ImageUrl = "abc.test.com/abc2.png",
                Scorings = new List<Scoring>()
                {
                    new ()
                    {
                        ScoringName = "taste",
                        ScoreValue = 1.4
                    },
                    new ()
                    {
                        ScoringName = "texture",
                        ScoreValue = 3.4
                    }
                }
            },
            new ()
            {
                Id = Guid.NewGuid(),
                ReviewerId = "test@test.com",
                ImageUrl = "abc.test.com/abc.png",
                Scorings = new List<Scoring>()
                {
                    new ()
                    {
                        ScoringName = "texture",
                        ScoreValue = 4.4
                    }
                }
            }
        });

    public readonly static List<BurgerPlaceTestData> HappyPathTestCases = new()
    {
        ContainingOneReview, ContainingMoreReviews
    };

    public BurgerPlace AsBurgerPlace() => new ()
    {
        Id = PlaceId,
        AvailableBurgers = AvailableBurgers,
        Information = Information,
        OpeningTime = OpeningTime,
        Location = Location,
        Reviews = Reviews
    };
}

