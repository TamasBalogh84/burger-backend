using System.Net;
using Atc.Rest.Results;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Results;

public class DeleteReviewResult : ResultBase
{
    private DeleteReviewResult(ActionResult result) : base(result) { }

    public static DeleteReviewResult Ok(string? message = null) => new(ResultFactory.CreateContentResult(HttpStatusCode.OK, message));

    public static DeleteReviewResult BadRequest(string? message = null) => new(ResultFactory.CreateContentResultWithValidationProblemDetails(HttpStatusCode.BadRequest, message));

    public static DeleteReviewResult NotFound(string? message = null) => new(ResultFactory.CreateContentResultWithProblemDetails(HttpStatusCode.NotFound, message));

    public static DeleteReviewResult InternalServerError(string? message = null) => new(ResultFactory.CreateContentResultWithProblemDetails(HttpStatusCode.InternalServerError, message));

    /// <summary>
    /// Performs an implicit conversion from CreateHourPerformanceUserInputResult to ActionResult.
    /// </summary>
    public static implicit operator DeleteReviewResult(string response) => Ok(response);
}