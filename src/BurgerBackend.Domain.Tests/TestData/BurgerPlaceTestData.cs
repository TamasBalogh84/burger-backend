using System;
using System.Collections.Generic;
using BurgerBackend.Domain.Entities.Cosmos;

namespace BurgerBackend.Api.Tests.TestData;

public record BurgerPlaceTestData(
    Guid PlaceId,
    IEnumerable<Burger> AvailableBurgers,
    string Name,
    string Information,
    Location Location,
    IEnumerable<OpeningTime> OpeningTimes,
    IEnumerable<Review> Reviews)
{
    public static readonly BurgerPlaceTestData ContainingOneReview = new(
        PlaceId: Guid.Parse("6b0f1d4d-9a17-4c2a-b5d5-b3874ee82999"),
        AvailableBurgers: new List<Burger>
        {
            new (Name: "test burger", Price: 10.0)
        },
        Name: "best place",
        Information: "this is a good place",
        Location: new Location
        (
            City: "test city",
            Address: "test str 12",
            Coordinates: "10,32.46 65,65.43"
        ),
        OpeningTimes: new List<OpeningTime>
        {
            new (
                Day: "Monday",
                OpeningStartTime: "10:00:00",
                OpeningEndTime: "20:00:00"
            )
        },
        Reviews: new List<Review>
        {
            new ()
            {
                Id = "74eb298e-1f26-420f-94d1-8d2fdd418385",
                ReviewerId = Guid.NewGuid().ToString(),
                ImageUrl = "abc.test.com/abc.png",
                ReviewText = "gooood",
                Scorings = new List<Scoring>
                {
                    new (
                        ScoringName: "texture",
                        ScoreValue: 4.4
                    )
                },
                CreatedDate = DateTimeOffset.Parse("2022-09-29 10:00:00")
            }
        });

    public static readonly BurgerPlaceTestData ContainingMoreReviews = new(
        PlaceId: Guid.Parse("36010a4f-653f-494f-a579-e630a1d3171f"),
        AvailableBurgers: new List<Burger>
        {
            new (Name: "test burger", Price: 10.0)
        },
        Name: "second best place",
        Information: "this is a good place",
        Location: new Location
        (
            City: "test city",
            Address: "test str 12",
            Coordinates: "10,32.46 65,65.43"
        ),
        OpeningTimes: new List<OpeningTime>
        {
            new (
                Day: "Saturday",
                OpeningStartTime: "10:00:00",
                OpeningEndTime: "23:00:00"
            )
        },
        Reviews: new List<Review>
        {
            new ()
            {
                Id = "a4bc17cd-8b1f-43c5-88e9-cae56423c7df",
                ReviewerId = Guid.NewGuid().ToString(),
                ImageUrl = "abc.test.com/abc2.png",
                ReviewText = "gooood",
                Scorings = new List<Scoring>
                {
                    new (
                        ScoringName: "texture",
                        ScoreValue: 4.5
                    ),
                    new (
                    ScoringName: "taste",
                    ScoreValue: 2
                    )
                },
                CreatedDate = DateTimeOffset.Parse("2022-09-29 16:00:00")
            },
            new ()
            {
                Id = "008c17dd-4175-451d-a9ee-abb1ffc21b21",
                ReviewerId = Guid.NewGuid().ToString(),
                ImageUrl = "abc.test.com/abc.png",
                ReviewText = "gooood",
                Scorings = new List<Scoring>
                {
                    new (
                        ScoringName: "texture",
                        ScoreValue: 4.4
                    )
                },
                CreatedDate = DateTimeOffset.Parse("2022-09-29 17:00:00")
            },
            new ()
            {
                Id = "0270592e-3e9b-40c3-b88a-cf915bcc257a",
                ReviewerId = Guid.NewGuid().ToString(),
                ImageUrl = "abc.test.com/abc2.png",
                ReviewText = "very good",
                Scorings = new List<Scoring>
                {
                    new (
                        ScoringName: "taste",
                        ScoreValue: 5
                    )
                },
                CreatedDate = DateTimeOffset.Parse("2022-09-29 17:30:00")
            }
        });

    public static readonly List<BurgerPlaceTestData> HappyPathTestCases = new()
    {
        ContainingOneReview,
        ContainingMoreReviews
    };

    public static readonly List<BurgerPlaceTestData> MultipleReviewTestCase = new()
    {
        ContainingMoreReviews
    };

    public BurgerPlace AsBurgerPlace() => new()
    {
        Id = PlaceId.ToString(),
        AvailableBurgers = AvailableBurgers,
        Information = Information,
        OpeningTimes = OpeningTimes,
        Location = Location,
        Reviews = Reviews
    };
}

