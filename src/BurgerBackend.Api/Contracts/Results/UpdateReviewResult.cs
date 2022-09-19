using System.Net;
using Atc.Rest.Results;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Results
{
    public class UpdateReviewResult :ResultBase
    {
            private UpdateReviewResult(ActionResult result) : base(result) { }

            public static UpdateReviewResult Ok(string? message = null) => new(ResultFactory.CreateContentResult(HttpStatusCode.OK, message));

            public static UpdateReviewResult BadRequest(string? message = null) => new(ResultFactory.CreateContentResultWithValidationProblemDetails(HttpStatusCode.BadRequest, message));

            public static UpdateReviewResult NotFound(string? message = null) => new(ResultFactory.CreateContentResultWithProblemDetails(HttpStatusCode.NotFound, message));

            public static UpdateReviewResult InternalServerError(string? message = null) => new(ResultFactory.CreateContentResultWithProblemDetails(HttpStatusCode.InternalServerError, message));

            /// <summary>
            /// Performs an implicit conversion from CreateHourPerformanceUserInputResult to ActionResult.
            /// </summary>
            public static implicit operator UpdateReviewResult(string response) => Ok(response);
        }
}
