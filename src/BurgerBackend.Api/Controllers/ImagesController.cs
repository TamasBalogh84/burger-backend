using BurgerBackend.Api.Contracts.Handlers.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Controllers;

[Authorize]
[ApiController]
[Route("/api/v1/images")]
public class ImagesController
{
    [HttpPost, DisableRequestSizeLimit]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AddImage([FromForm] IFormFile file, [FromServices] IAddImageHandler handler, CancellationToken cancellationToken)
    {
        if (handler is null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        return await handler.ExecuteAsync(file, cancellationToken);
    }
}
