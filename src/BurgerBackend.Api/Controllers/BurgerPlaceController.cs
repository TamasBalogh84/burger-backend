using BurgerBackend.Domain.Entities.Cosmos;
using BurgerBackend.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/v1/burgerplaces")]
    public class BurgerPlaceController : ControllerBase
    {
        [HttpGet()]
        [ProducesResponseType(typeof(List<BurgerPlace>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAllPlaces([FromServices] IGetAllPlacesHandler handler, CancellationToken cancellationToken)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return await handler.ExecuteAsync(cancellationToken);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<BurgerPlace>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetPlaceById([FromServices] IGetPlaceByIdHandler handler, CancellationToken cancellationToken)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return await handler.ExecuteAsync(cancellationToken);
        }
    }
}
