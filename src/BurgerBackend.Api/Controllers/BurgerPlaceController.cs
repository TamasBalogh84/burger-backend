using BurgerBackend.Api.Contracts.Handlers.Abstract;
using BurgerBackend.Api.Contracts.Handlers.Concrete;
using BurgerBackend.Api.Contracts.Models;
using BurgerBackend.Api.Contracts.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Controllers;

//[Authorize]
[ApiController]
[Route("/api/v1/burger-places")]
public class BurgerPlacesController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<BurgerPlace>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetAllPlaces([FromQuery] GetAllPlacesParameters parameters, [FromServices] IGetAllPlacesHandler handler, CancellationToken cancellationToken)
    {
        if (handler is null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        return await handler.ExecuteAsync(parameters.SkipReviews, cancellationToken);
    }

    [HttpPost]
    [ProducesResponseType(typeof(List<BurgerPlace>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreatePlace(CreatePlaceParameters parameters, [FromServices] ICreatePlaceHandler handler, CancellationToken cancellationToken)
    {
        if (handler is null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        return await handler.ExecuteAsync(parameters, cancellationToken);
    }

    [HttpGet("{placeId}")]
    [ProducesResponseType(typeof(BurgerPlace), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetPlaceById([FromRoute] GetPlaceByIdParameters parameters, [FromServices] IGetPlaceByIdHandler handler, CancellationToken cancellationToken)
    {
        if (handler is null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        return await handler.ExecuteAsync(parameters, cancellationToken);
    }

    [HttpGet("{placeId}/reviews")]
    [ProducesResponseType(typeof(List<Review>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetReviewsByPlaceId([FromRoute] GetReviewsByPlaceIdParameters parameters, [FromServices] IGetReviewsByPlaceIdHandler handler, CancellationToken cancellationToken)
    {
        if (handler is null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        return await handler.ExecuteAsync(parameters, cancellationToken);
    }

    [HttpGet("{placeId}/reviews/{reviewId}")]
    [ProducesResponseType(typeof(Review), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetReviewById([FromRoute] GetReviewByIdParameters parameters, [FromServices] IGetReviewByIdHandler handler, CancellationToken cancellationToken)
    {
        if (handler is null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        return await handler.ExecuteAsync(parameters, cancellationToken);
    }

    //[ValidateAntiForgeryToken]
    [HttpPost("{placeId}/reviews")]
    [ProducesResponseType(typeof(Review), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateReviewAsync([FromRoute] CreateReviewParameters parameters, [FromServices] ICreateReviewHandler handler, CancellationToken cancellationToken)
    {
        if (handler is null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        return await handler.ExecuteAsync(parameters, cancellationToken);
    }


    //[ValidateAntiForgeryToken]
    [HttpPut("{placeId}/reviews/{reviewId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateReviewAsync([FromRoute] UpdateReviewParameters parameters, [FromServices] IUpdateReviewHandler handler, CancellationToken cancellationToken)
    {
        if (handler is null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        return await handler.ExecuteAsync(parameters, cancellationToken);
    }
    //[ValidateAntiForgeryToken]
    [HttpDelete("{placeId}/reviews/{reviewId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteReviewAsync([FromRoute] DeleteReviewParameters parameters, [FromServices] IDeleteReviewHandler handler, CancellationToken cancellationToken)
    {
        if (handler is null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        return await handler.ExecuteAsync(parameters, cancellationToken);
    }
}