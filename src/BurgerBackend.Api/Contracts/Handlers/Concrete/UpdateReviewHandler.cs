using BurgerBackend.Api.Contracts.Extensions;
using BurgerBackend.Api.Contracts.Handlers.Abstract;
using BurgerBackend.Api.Contracts.Parameters;
using BurgerBackend.Api.Contracts.Results;
using BurgerBackend.Api.Extensions;
using BurgerBackend.Domain.Entities.Cosmos;
using BurgerBackend.Domain.Repositories.Cosmos;

namespace BurgerBackend.Api.Contracts.Handlers.Concrete;

public class UpdateReviewHandler : IUpdateReviewHandler
{
    private readonly IBurgerPlacesRepository _burgerPlacesRepository;
    private readonly ILogger<UpdateReviewHandler> _logger;

    public UpdateReviewHandler(IBurgerPlacesRepository burgerPlacesRepository, ILogger<UpdateReviewHandler> logger)
    {
        _burgerPlacesRepository = burgerPlacesRepository ?? throw new ArgumentNullException(nameof(burgerPlacesRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<UpdateReviewResult> ExecuteAsync(UpdateReviewParameters parameters, CancellationToken cancellationToken = default)
    {
        try
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (!Guid.TryParse(parameters.PlaceId.ToString(), out _) || parameters.PlaceId == Guid.Empty)
            {
                const string logMessage = "Invalid Guid!";
                _logger.LogWarning(logMessage);
                return UpdateReviewResult.BadRequest(logMessage);
            }

            var place = await _burgerPlacesRepository.GetByIdAsync(parameters.PlaceId.ToString(), cancellationToken);

            var review = place?.Reviews?.FirstOrDefault(r => r.Id == parameters.ReviewId.ToString());

            if (place is null || review is null)
            {
                return UpdateReviewResult.NotFound($"No data found to update PlaceID: {parameters.PlaceId} ReviewID: {parameters.ReviewId}");
            }

            var updatedReview = new Review
            {
                Id = review.Id,
                ReviewerId = review.ReviewerId,
                ReviewText = parameters.ReviewRequest.ReviewText,
                Scorings = parameters.ReviewRequest.Scorings.Select(s => s.ToScoring()),
                ImageUrl = parameters.ReviewRequest.ImageUrl,
                CreatedDate = review.CreatedDate
            };

            place.Reviews = place.Reviews?.Replace(r => r.Id == parameters.ReviewId.ToString(), updatedReview);

            var result = await _burgerPlacesRepository.ReplaceAsync(place, parameters.PlaceId.ToString(), cancellationToken);

            return UpdateReviewResult.Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return UpdateReviewResult.InternalServerError(e.Message);
        }
    }
}