using BurgerBackend.Api.Contracts.Handlers.Abstract;
using BurgerBackend.Api.Contracts.Handlers.Concrete;
using BurgerBackend.Api.Contracts.Models;
using BurgerBackend.Api.Contracts.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Controllers;

[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("/api/v1/burger-places")]
public class BurgerPlacesController : ControllerBase
{
    [HttpGet("burger-places")]
    [ProducesResponseType(typeof(List<BurgerPlace>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetAllPlaces([FromQuery] GetAllPlacesParameters parameters, [FromServices] IGetAllPlacesHandler handler, CancellationToken cancellationToken)
    {
        if (handler is null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        return await handler.ExecuteAsync(parameters, cancellationToken);
    }

    [HttpPost("burger-places")]
    [ProducesResponseType(typeof(List<BurgerPlace>), StatusCodes.Status201Created)]
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

    [HttpGet("burger-places/{placeId}")]
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

    [HttpPut("burger-places/{placeId}")]
    [ProducesResponseType(typeof(BurgerPlace), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdatePlace(/*UpdatePlaceParameters parameters, [FromServices] IUpdatePlaceHandler handler, CancellationToken cancellationToken*/)
    {
        //if (handler is null)
        //{
        //    throw new ArgumentNullException(nameof(handler));
        //}

        //return await handler.ExecuteAsync(parameters, cancellationToken);

        return Ok();
    }

    [HttpDelete("burger-places/{placeId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeletePlace(/*DeletePlaceParameters parameters, [FromServices] IDeletePlaceHandler handler, CancellationToken cancellationToken*/)
    {
    //    if (handler is null)
    //    {
    //        throw new ArgumentNullException(nameof(handler));
    //    }

    //    return await handler.ExecuteAsync(parameters, cancellationToken);

        return Ok();
    }

    [HttpGet("burger-places/{placeId}/reviews")]
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

    [HttpGet("burger-places/{placeId}/reviews/{reviewId}")]
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
    [HttpPost("burger-places/{placeId}/reviews")]
    [ProducesResponseType(typeof(Review), StatusCodes.Status201Created)]
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
    [HttpPut("burger-places/{placeId}/reviews/{reviewId}")]
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
    [HttpDelete("burger-places/{placeId}/reviews/{reviewId}")]
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