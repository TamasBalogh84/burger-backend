using BurgerBackend.Api.Contracts.Extensions;
using BurgerBackend.Api.Contracts.Handlers.Abstract;
using BurgerBackend.Api.Contracts.Parameters;
using BurgerBackend.Api.Contracts.Results;
using BurgerBackend.Domain.Repositories.Cosmos;

namespace BurgerBackend.Api.Contracts.Handlers.Concrete
{
    public class GetPlaceByIdHandler : IGetPlaceByIdHandler
    {
        private readonly IBurgerPlacesRepository _burgerPlacesRepository;
        private readonly ILogger<GetPlaceByIdHandler> _logger;

        public GetPlaceByIdHandler(IBurgerPlacesRepository burgerPlacesRepository, ILogger<GetPlaceByIdHandler> logger)
        {
            _burgerPlacesRepository = burgerPlacesRepository ?? throw new ArgumentNullException(nameof(burgerPlacesRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<GetPlayceByIdResult> ExecuteAsync(GetPlaceByIdParameters parameters, CancellationToken cancellationToken = default)
        {
            try
            {
                if (parameters is null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                if (!Guid.TryParse(parameters.PlaceId.ToString(), out _))
                {
                    const string logMessage = "Invalid Guid!";
                    _logger.LogWarning(logMessage);
                    return GetPlayceByIdResult.BadRequest(logMessage);
                }

                var result = await _burgerPlacesRepository.GetByIdAsync(parameters.PlaceId.ToString(), cancellationToken);

                if (result is null)
                {
                    var logMessage = $"No burger place with Id {parameters.PlaceId} found!";
                    _logger.LogInformation(logMessage);
                    return GetPlayceByIdResult.NotFound(logMessage);
                }

                return GetPlayceByIdResult.Ok(result.ToBurgerPlace());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return GetPlayceByIdResult.InternalServerError(e.Message);
            }
        }
    }
}