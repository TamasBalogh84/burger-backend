using System;
using BurgerBackend.Api.Contracts.Parameters;

namespace BurgerBackend.Api.Tests.TestData;

public static class GetReviewsByPlaceIdParametersTestData
{
    public static readonly GetReviewsByPlaceIdParameters ValidGetReviewsByPlaceIdParameters = new ()
    {
        PlaceId = BurgerPlaceTestData.ContainingMoreReviews.PlaceId
    };

    public static readonly GetReviewsByPlaceIdParameters ValidGetReviewsByPlaceIdParametersWithPagination = new()
    {
        PlaceId = BurgerPlaceTestData.ContainingMoreReviews.PlaceId,
        PageNumber = 1,
        PageSize = 2
    };

    public static readonly GetReviewsByPlaceIdParameters GetReviewsByPlaceIdParametersWithNotExistingPlace = new()
    {
        PlaceId = Guid.NewGuid()
    };

    public static readonly GetReviewsByPlaceIdParameters InValidGetReviewsByPlaceIdParameters = new()
    {
        PlaceId = Guid.Empty
    };
}