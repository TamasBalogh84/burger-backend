using System;
using System.Collections.Generic;
using System.Linq;
using BurgerBackend.Api.Contracts.Models;
using BurgerBackend.Api.Contracts.Parameters;

namespace BurgerBackend.Api.Tests.TestData;

public static class UpdateReviewParametersTestData
{
    public static readonly UpdateReviewParameters ValidUpdateReviewParameters = new(new ReviewRequest
    (
        ImageUrl: "www.test.com/123.png",
        ReviewText: "changed text",
        Scorings: new List<Scoring>()
        {
            new(ScoringName: "texture", ScoreValue: 2.9)
        }
    ))
    {
        PlaceId = BurgerPlaceTestData.ContainingMoreReviews.PlaceId,
        ReviewId = Guid.Parse((ReadOnlySpan<char>)BurgerPlaceTestData.ContainingMoreReviews.Reviews.FirstOrDefault()?.Id)
    };

    public static readonly UpdateReviewParameters NotExistingUpdateReviewParameters = new(new ReviewRequest
    (
        ImageUrl: "www.test.com/123.png",
        ReviewText: "changed text",
        Scorings: new List<Scoring>()
        {
            new(ScoringName: "texture", ScoreValue: 2.9)
        }
    ))
    {
        PlaceId = Guid.NewGuid(),
        ReviewId = Guid.NewGuid()
    };

    public static readonly UpdateReviewParameters InvalidUpdateReviewParameters = new(new ReviewRequest
    (
        ImageUrl: "www.test.com/123.png",
        ReviewText: "changed text",
        Scorings: new List<Scoring>()
        {
            new(ScoringName: "texture", ScoreValue: 2.9)
        }
    ))
    {
        PlaceId = Guid.Empty,
        ReviewId = Guid.Parse((ReadOnlySpan<char>)BurgerPlaceTestData.ContainingMoreReviews.Reviews.FirstOrDefault()?.Id)
    };

}