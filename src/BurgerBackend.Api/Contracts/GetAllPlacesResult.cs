using Atc.Rest.Results;
using BurgerBackend.Domain.Entities.Cosmos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BurgerBackend.Api.Contracts
{
    public class GetAllPlacesResult : ResultBase
    {
        private GetAllPlacesResult(ActionResult result) : base(result) { }

        /// <summary>
        /// 200 - Ok response.
        /// </summary>
        public static GetAllPlacesResult Ok(IEnumerable<BurgerPlace> response) => new GetAllPlacesResult(new OkObjectResult(response ?? Enumerable.Empty<BurgerPlace>()));

        /// <summary>
        /// 404 - NotFound response.
        /// </summary>
        public static GetAllPlacesResult NotFound(string? message = null) => new GetAllPlacesResult(ResultFactory.CreateContentResultWithProblemDetails(HttpStatusCode.NotFound, message));

        /// <summary>
        /// Performs an implicit conversion from GetOrdersByQueryResult to ActionResult.
        /// </summary>
        public static implicit operator GetAllPlacesResult(List<BurgerPlace> response) => Ok(response);
    }
}
