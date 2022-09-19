using System.Net;
using Atc.Rest.Results;
using BurgerBackend.Api.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Results
{
    public class GetPlayceByIdResult : ResultBase
    {
        private GetPlayceByIdResult(ActionResult result) : base(result) { }

        public static GetPlayceByIdResult Ok(BurgerPlace? response) => new(new OkObjectResult(response));

        public static GetPlayceByIdResult BadRequest(string? message = null) => new GetPlayceByIdResult(ResultFactory.CreateContentResultWithValidationProblemDetails(HttpStatusCode.BadRequest, message));


        public static GetPlayceByIdResult NotFound(string? message = null) => new(ResultFactory.CreateContentResultWithProblemDetails(HttpStatusCode.NotFound, message));

        public static GetPlayceByIdResult InternalServerError(string? message = null) => new(ResultFactory.CreateContentResultWithProblemDetails(HttpStatusCode.InternalServerError, message));

        public static implicit operator GetPlayceByIdResult(BurgerPlace? response) => Ok(response);
    }
}
