using BurgerBackend.Api.Contracts.Handlers.Abstract;
using BurgerBackend.Api.Contracts.Parameters;
using BurgerBackend.Api.Contracts.Results;
using BurgerBackend.Domain.Repositories.Cosmos;

namespace BurgerBackend.Api.Contracts.Handlers.Concrete;

public class DeleteReviewHandler : IDeleteReviewHandler
{
    private readonly IBurgerPlacesRepository _burgerPlacesRepository;
    private readonly ILogger<DeleteReviewHandler> _logger;

    public DeleteReviewHandler(IBurgerPlacesRepository burgerPlacesRepository, ILogger<DeleteReviewHandler> logger)
    {
        _burgerPlacesRepository = burgerPlacesRepository ?? throw new ArgumentNullException(nameof(burgerPlacesRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<DeleteReviewResult> ExecuteAsync(DeleteReviewParameters parameters, CancellationToken cancellationToken = default)
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
                return DeleteReviewResult.BadRequest(logMessage);
            }

            var place = await _burgerPlacesRepository.GetByIdAsync(parameters.PlaceId.ToString(), cancellationToken);

            var review = place?.Reviews.FirstOrDefault(r => r.Id == parameters.ReviewId.ToString());

            if (place is null || review is null)
            {
                return DeleteReviewResult.NotFound($"No data found to delete Place ID: {parameters.PlaceId} Review ID: {parameters.ReviewId}");
            }

            place.Reviews = place.Reviews.Where(r => r.Id != parameters.ReviewId.ToString());

            await _burgerPlacesRepository.StoreAsync(place, cancellationToken);

            return DeleteReviewResult.Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return DeleteReviewResult.InternalServerError(e.Message);
        }
    }
}