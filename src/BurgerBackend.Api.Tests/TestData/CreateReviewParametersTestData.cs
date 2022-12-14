using System;
using System.Collections.Generic;
using BurgerBackend.Api.Contracts.Models;
using BurgerBackend.Api.Contracts.Parameters;

namespace BurgerBackend.Api.Tests.TestData;

public static class CreateReviewParametersTestData
{
    public static readonly CreateReviewParameters ValidCreateReviewParameters = new (new CreateReview
    {
        ReviewerId = Guid.NewGuid().ToString(),
        ImageUrl = "www.test.com",
        Scorings = new List<Scoring>()
        {
            new(ScoringName: "texture", ScoreValue: 4.8)
        }
    })
    {
        PlaceId = new Guid("b32ca706-c835-47a6-9e8c-cf889293e507"),
    };

    public static readonly CreateReviewParameters InvalidCreateReviewParameters = new(new CreateReview
    {
        ReviewerId = Guid.NewGuid().ToString(),
        ImageUrl = "www.test.com",
        Scorings = new List<Scoring>()
        {
            new(ScoringName: "texture", ScoreValue: 4.8)
        }
    })
    {
        PlaceId = Guid.Empty
    };
}