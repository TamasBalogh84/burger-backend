using System.Net;
using Atc.Rest.Results;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Results
{
    public class AddImageResult : ResultBase
    {
        public AddImageResult(ActionResult result) : base(result)
        {
        }

        public static AddImageResult Ok(string? response = null) => new(new OkObjectResult(response));

        public static AddImageResult BadRequest(string? message = null) => new(ResultFactory.CreateContentResultWithValidationProblemDetails(HttpStatusCode.BadRequest, message));

        public static AddImageResult InternalServerError(string? message = null) => new(ResultFactory.CreateContentResultWithProblemDetails(HttpStatusCode.InternalServerError, message));

        /// <summary>
        /// Performs an implicit conversion from CreateHourPerformanceUserInputResult to ActionResult.
        /// </summary>
        public static implicit operator AddImageResult(string response) => Ok(response);
    }
}
