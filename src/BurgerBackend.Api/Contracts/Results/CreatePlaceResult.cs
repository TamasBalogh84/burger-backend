using System.Net;
using Atc.Rest.Results;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Results;

public class CreatePlaceResult : ResultBase
{
    private CreatePlaceResult(ActionResult result) : base(result) { }

    public static CreatePlaceResult Ok(string? response) => new(new OkObjectResult(response));

    public static CreatePlaceResult BadRequest(string? message = null) => new(ResultFactory.CreateContentResultWithValidationProblemDetails(HttpStatusCode.BadRequest, message));

    public static CreatePlaceResult InternalServerError(string? message = null) => new(ResultFactory.CreateContentResultWithProblemDetails(HttpStatusCode.InternalServerError, message));

    /// <summary>
    /// Performs an implicit conversion from CreateHourPerformanceUserInputResult to ActionResult.
    /// </summary>
    public static implicit operator CreatePlaceResult(string response) => Ok(response);
}