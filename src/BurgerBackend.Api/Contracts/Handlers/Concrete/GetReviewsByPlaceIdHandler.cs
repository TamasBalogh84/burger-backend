using BurgerBackend.Api.Contracts.Extensions;
using BurgerBackend.Api.Contracts.Handlers.Abstract;
using BurgerBackend.Api.Contracts.Parameters;
using BurgerBackend.Api.Contracts.Results;
using BurgerBackend.Domain.Repositories.Cosmos;

namespace BurgerBackend.Api.Contracts.Handlers.Concrete;

public class GetReviewsByPlaceIdHandler : IGetReviewsByPlaceIdHandler
{
    private readonly IBurgerPlacesRepository _burgerPlacesRepository;
    private readonly ILogger<GetReviewsByPlaceIdHandler> _logger;

    public GetReviewsByPlaceIdHandler(IBurgerPlacesRepository burgerPlacesRepository, ILogger<GetReviewsByPlaceIdHandler> logger)
    {
        _burgerPlacesRepository = burgerPlacesRepository ?? throw new ArgumentNullException(nameof(burgerPlacesRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<GetReviewsByPlaceIdResult> ExecuteAsync(GetReviewsByPlaceIdParameters parameters, CancellationToken cancellationToken = default)
    {
        try
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (!Guid.TryParse(parameters.PlaceId.ToString(), out _) || parameters.PlaceId == Guid.Empty)
            {
                var logMessage = "Invalid Guid!";
                _logger.LogWarning(logMessage);
                return GetReviewsByPlaceIdResult.BadRequest(logMessage);
            }

            var result = await _burgerPlacesRepository
                .GetReviewsByPlaceIdAsync(parameters.PlaceId, cancellationToken);

            if (!result.Any())
            {
                var logMessage = $"No reviews for Place ID: {parameters.PlaceId} found!";
                _logger.LogInformation(logMessage);
                return GetReviewsByPlaceIdResult.NotFound(logMessage);
            }

            var pagedResult = result
                .OrderBy(r => r.CreatedDate)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);

            return GetReviewsByPlaceIdResult.Ok(pagedResult.Select(r => r.ToReview()));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return GetReviewsByPlaceIdResult.InternalServerError(e.Message);
        }
    }
}