using BurgerBackend.Api.Contracts.Extensions;
using BurgerBackend.Api.Contracts.Parameters;
using BurgerBackend.Api.Contracts.Results;
using BurgerBackend.Domain.Repositories.Cosmos;

namespace BurgerBackend.Api.Contracts.Handlers.Concrete;

public class CreatePlaceHandler : ICreatePlaceHandler
{
    private readonly IBurgerPlacesRepository _burgerPlacesRepository;
    private readonly ILogger<CreatePlaceHandler> _logger;

    public CreatePlaceHandler(IBurgerPlacesRepository burgerPlacesRepository, ILogger<CreatePlaceHandler> logger)
    {
        _burgerPlacesRepository = burgerPlacesRepository ?? throw new ArgumentNullException(nameof(burgerPlacesRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<CreatePlaceResult> ExecuteAsync(CreatePlaceParameters parameters, CancellationToken cancellationToken = default)
    {
        try
        {
            if (parameters.Place is null)
            {
                const string logMessage = "Invalid parameters!";
                _logger.LogWarning(logMessage);
                return CreatePlaceResult.BadRequest(logMessage);
            }

            var result = await _burgerPlacesRepository.StoreAsync(parameters.Place.ToBurgerPlace(), cancellationToken);

            return CreatePlaceResult.Ok(result.Id);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return CreatePlaceResult.InternalServerError(e.Message);
        }
    }
}
