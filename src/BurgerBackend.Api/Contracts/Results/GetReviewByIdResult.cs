using System.Net;
using Atc.Rest.Results;
using BurgerBackend.Api.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Results;

public class GetReviewByIdResult : ResultBase
{
    private GetReviewByIdResult(ActionResult result) : base(result) { }

    public static GetReviewByIdResult Ok(Review? response) => new(new OkObjectResult(response));

    public static GetReviewByIdResult BadRequest(string? message = null) => new (ResultFactory.CreateContentResultWithValidationProblemDetails(HttpStatusCode.BadRequest, message));


    public static GetReviewByIdResult NotFound(string? message = null) => new(ResultFactory.CreateContentResultWithProblemDetails(HttpStatusCode.NotFound, message));

    public static GetReviewByIdResult InternalServerError(string? message = null) => new(ResultFactory.CreateContentResultWithProblemDetails(HttpStatusCode.InternalServerError, message));

    public static implicit operator GetReviewByIdResult(Review? response) => Ok(response);
}