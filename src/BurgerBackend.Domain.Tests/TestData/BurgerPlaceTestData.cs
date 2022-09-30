//using System;
//using System.Collections.Generic;
//using BurgerBackend.Domain.Entities.Cosmos;

//namespace BurgerBackend.Api.Tests.TestData;

//public record BurgerPlaceTestData (
//    Guid PlaceId,
//    IEnumerable<Burger> AvailableBurgers,
//    string Name,
//    string Information,
//    Location Location,
//    IEnumerable<OpeningTime> OpeningTimes,
//    IEnumerable<Review> Reviews)
//{
//    public static readonly BurgerPlaceTestData ContainingOneReview = new(
//        PlaceId: Guid.NewGuid(),
//        AvailableBurgers: new List<Burger>
//        {
//            new (Name: "test burger", Price: 10.0)
//            //{
//            //    Name = "test burger",
//            //    Price = 10.0
//            //}
//        },
//        Name: "best place",
//        Information: "this is a good place",
//        Location: new Location
//        {
//            City = "test city",
//            Address = "test str 12",
//            Coordinates = "10,32.46 65,65.43"
//        },
//        OpeningTimes: new List<OpeningTime>
//        {
//            new ()
//            {
//                Day = "Monday",
//                OpeningStartTime = "10:00:00",
//                OpeningEndTime = "20:00:00"
//            }
//        },
//        Reviews: new List<Review>
//        {
//            new ()
//            {
//                Id = Guid.NewGuid().ToString(),
//                ReviewerId = Guid.NewGuid().ToString(),
//                ImageUrl = "abc.test.com/abc.png",
//                Scorings = new List<Scoring>
//                {
//                    new ()
//                    {
//                        ScoringName = "texture",
//                        ScoreValue = 4.4
//                    }
//                },
//                CreatedDate = DateTimeOffset.Parse("2022-09-29 10:00:00")
//            }
//        });

//    public static readonly BurgerPlaceTestData ContainingMoreReviews = new(
//        PlaceId: Guid.NewGuid(),
//        AvailableBurgers: new List<Burger>
//        {
//            new (Name: "test burger", Price: 10.0)
//            //{
//            //    Name = "test burger",
//            //    Price = 10.0
//            //}
//        },
//        Name: "second best place",
//        Information: "this is a good place",
//        Location: new Location
//        {
//            City = "test city",
//            Address = "test str 12",
//            Coordinates = "10,32.46 65,65.43"
//        },
//        OpeningTimes: new List<OpeningTime>
//        {
//            new ()
//            {
//                Day = "Saturday",
//                OpeningStartTime = "10:00:00",
//                OpeningEndTime = "22:00:00"
//            }
//        },
//        Reviews: new List<Review>
//        {
//            new ()
//            {
//                Id = Guid.NewGuid().ToString(),
//                ReviewerId = Guid.NewGuid().ToString(),
//                ImageUrl = "abc.test.com/abc2.png",
//                Scorings = new List<Scoring>()
//                {
//                    new ()
//                    {
//                        ScoringName = "taste",
//                        ScoreValue = 1.4
//                    },
//                    new ()
//                    {
//                        ScoringName = "texture",
//                        ScoreValue = 3.4
//                    }
//                },
//                CreatedDate = DateTimeOffset.Parse("2022-09-29 17:00:00")
//            },
//            new ()
//            {
//                Id = Guid.NewGuid().ToString(),
//                ReviewerId = Guid.NewGuid().ToString(),
//                ImageUrl = "abc.test.com/abc.png",
//                Scorings = new List<Scoring>()
//                {
//                    new ()
//                    {
//                        ScoringName = "texture",
//                        ScoreValue = 4.4
//                    }
//                },
//                CreatedDate = DateTimeOffset.Parse("2022-09-29 16:00:00")
//            }
//        });

//    public static readonly List<BurgerPlaceTestData> HappyPathTestCases = new()
//    {
//        ContainingOneReview, ContainingMoreReviews
//    };

//    public BurgerPlace AsBurgerPlace() => new ()
//    {
//        Id = PlaceId.ToString(),
//        AvailableBurgers = AvailableBurgers,
//        Information = Information,
//        OpeningTimes = OpeningTimes,
//        Location = Location,
//        Reviews = Reviews
//    };
//}

