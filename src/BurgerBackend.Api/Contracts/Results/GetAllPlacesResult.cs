using System.Net;
using Atc.Rest.Results;
using BurgerBackend.Api.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Results;

public class GetAllPlacesResult : ResultBase
{
    private GetAllPlacesResult(ActionResult result) : base(result) { }

    public static GetAllPlacesResult Ok(IEnumerable<BurgerPlace> response) => new (new OkObjectResult(response ?? Enumerable.Empty<BurgerPlace>()));

    public static GetAllPlacesResult NotFound(string? message = null) => new (ResultFactory.CreateContentResultWithProblemDetails(HttpStatusCode.NotFound, message));

    public static GetAllPlacesResult InternalServerError(string? message = null) => new (ResultFactory.CreateContentResultWithProblemDetails(HttpStatusCode.InternalServerError, message));

    public static implicit operator GetAllPlacesResult(List<BurgerPlace> response) => Ok(response);
}