using System.Net;
using Atc.Rest.Results;
using BurgerBackend.Api.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Results;

public class CreateReviewResult :ResultBase
{
    private CreateReviewResult(ActionResult result) : base(result) { }

    public static CreateReviewResult Ok(Review response) => new (new OkObjectResult(response));

    public static CreateReviewResult BadRequest(string? message = null) => new (ResultFactory.CreateContentResultWithValidationProblemDetails(HttpStatusCode.BadRequest, message));

    public static CreateReviewResult NotFound(string? message = null) => new(ResultFactory.CreateContentResultWithProblemDetails(HttpStatusCode.NotFound, message));

    public static CreateReviewResult InternalServerError(string? message = null) => new (ResultFactory.CreateContentResultWithProblemDetails(HttpStatusCode.InternalServerError, message));

    /// <summary>
    /// Performs an implicit conversion from CreateHourPerformanceUserInputResult to ActionResult.
    /// </summary>
    public static implicit operator CreateReviewResult(Review response) => Ok(response);
}