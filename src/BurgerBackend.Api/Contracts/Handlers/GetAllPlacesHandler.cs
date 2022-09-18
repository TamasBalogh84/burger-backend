using BurgerBackend.Api.Contracts;
using BurgerBackend.Domain.Repositories.Cosmos;

namespace BurgerBackend.Domain.Handlers
{
    public class GetAllPlacesHandler : IGetAllPlacesHandler
    {
        private readonly IBurgerPlacesRepository _burgerPlacesRepository;
        private readonly ILogger<GetAllPlacesHandler> _logger;

        public GetAllPlacesHandler (IBurgerPlacesRepository burgerPlacesRepository, ILogger<GetAllPlacesHandler> logger)
        {
            this._burgerPlacesRepository = burgerPlacesRepository ?? throw new ArgumentNullException(nameof(burgerPlacesRepository));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<GetAllPlacesResult> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var result = await _burgerPlacesRepository.GetAllAsync(cancellationToken: cancellationToken);

            if (!result.Any())
            {
                _logger.LogInformation("No burger places found!");
                return GetAllPlacesResult.NotFound("No burger places found!");
            }

            return GetAllPlacesResult.Ok(result);
        }
    }
}
