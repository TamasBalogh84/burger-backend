using BurgerBackend.Api.Contracts.Extensions;
using BurgerBackend.Api.Contracts.Handlers.Abstract;
using BurgerBackend.Api.Contracts.Parameters;
using BurgerBackend.Api.Contracts.Results;
using BurgerBackend.Domain.Repositories.Cosmos;

namespace BurgerBackend.Api.Contracts.Handlers.Concrete;

public class GetAllPlacesHandler : IGetAllPlacesHandler
{
    private readonly IBurgerPlacesRepository _burgerPlacesRepository;
    private readonly ILogger<GetAllPlacesHandler> _logger;

    public GetAllPlacesHandler (IBurgerPlacesRepository burgerPlacesRepository, ILogger<GetAllPlacesHandler> logger)
    {
        _burgerPlacesRepository = burgerPlacesRepository ?? throw new ArgumentNullException(nameof(burgerPlacesRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<GetAllPlacesResult> ExecuteAsync(GetAllPlacesParameters parameters, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _burgerPlacesRepository
                .GetAllAsync(parameters.SkipReviews, parameters.PageNumber, parameters.PageSize, cancellationToken);

            if (result.Any()) return GetAllPlacesResult.Ok(result.Select(b => b.ToBurgerPlace()));

            var logMessage = "No burger places found!";
            _logger.LogInformation(logMessage);
            return GetAllPlacesResult.NotFound(logMessage);

        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return GetAllPlacesResult.InternalServerError(e.Message);
        }
    }
}