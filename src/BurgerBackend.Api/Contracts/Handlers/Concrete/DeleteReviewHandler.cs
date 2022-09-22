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

    public async Task<UpdateReviewResult> ExecuteAsync(DeleteReviewParameters parameters, CancellationToken cancellationToken = default)
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
                return UpdateReviewResult.BadRequest(logMessage);
            }

            var place = await _burgerPlacesRepository.GetByIdAsync(parameters.PlaceId.ToString(), cancellationToken);

            var review = place?.Reviews.First(r => r.Id == parameters.ReviewId);

            if (place is null || review is null)
            {
                return UpdateReviewResult.NotFound($"No data found to delete {parameters.PlaceId} {parameters.ReviewId}");
            }

            place.Reviews.ToList().Remove(review);

            await _burgerPlacesRepository.StoreAsync(place, cancellationToken);

            return UpdateReviewResult.Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return UpdateReviewResult.InternalServerError(e.Message);
        }
    }
}