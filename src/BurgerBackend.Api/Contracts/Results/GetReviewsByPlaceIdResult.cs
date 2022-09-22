using System.Net;
using Atc.Rest.Results;
using BurgerBackend.Api.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Results;

public class GetReviewsByPlaceIdResult : ResultBase
{
    private GetReviewsByPlaceIdResult(ActionResult result) : base(result) { }

    public static GetReviewsByPlaceIdResult Ok(IEnumerable<Review> response) => new(new OkObjectResult(response));

    public static GetReviewsByPlaceIdResult BadRequest(string? message = null) => new GetReviewsByPlaceIdResult(ResultFactory.CreateContentResultWithValidationProblemDetails(HttpStatusCode.BadRequest, message));


    public static GetReviewsByPlaceIdResult NotFound(string? message = null) => new(ResultFactory.CreateContentResultWithProblemDetails(HttpStatusCode.NotFound, message));

    public static GetReviewsByPlaceIdResult InternalServerError(string? message = null) => new(ResultFactory.CreateContentResultWithProblemDetails(HttpStatusCode.InternalServerError, message));

    public static implicit operator GetReviewsByPlaceIdResult(List<Review> response) => Ok(response);
}