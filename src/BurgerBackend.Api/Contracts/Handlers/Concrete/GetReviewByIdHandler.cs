using BurgerBackend.Api.Contracts.Extensions;
using BurgerBackend.Api.Contracts.Handlers.Abstract;
using BurgerBackend.Api.Contracts.Parameters;
using BurgerBackend.Api.Contracts.Results;
using BurgerBackend.Domain.Repositories.Cosmos;

namespace BurgerBackend.Api.Contracts.Handlers.Concrete;

public class GetReviewByIdHandler : IGetReviewByIdHandler
{
    private readonly IBurgerPlacesRepository _burgerPlacesRepository;
    private readonly ILogger<GetReviewByIdHandler> _logger;

    public GetReviewByIdHandler(IBurgerPlacesRepository burgerPlacesRepository, ILogger<GetReviewByIdHandler> logger)
    {
        _burgerPlacesRepository = burgerPlacesRepository ?? throw new ArgumentNullException(nameof(burgerPlacesRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<GetReviewByIdResult> ExecuteAsync(GetReviewByIdParameters parameters, CancellationToken cancellationToken = default)
    {
        try
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (!Guid.TryParse(parameters.ReviewId.ToString(), out _))
            {
                const string logMessage = "Invalid Guid!";
                _logger.LogWarning(logMessage);
                return GetReviewByIdResult.BadRequest(logMessage);
            }

            var result = await _burgerPlacesRepository.GetReviewByIdAsync(parameters.PlaceId, parameters.ReviewId, cancellationToken);

            if (result is null)
            {
                var logMessage = $"No Review with Id {parameters.ReviewId} found!";
                _logger.LogInformation(logMessage);
                return GetReviewByIdResult.NotFound(logMessage);
            }

            return GetReviewByIdResult.Ok(result.ToReview());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return GetReviewByIdResult.InternalServerError(e.Message);
        }
    }
}