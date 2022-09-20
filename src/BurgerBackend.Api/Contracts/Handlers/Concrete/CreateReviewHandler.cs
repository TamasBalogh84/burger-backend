using BurgerBackend.Api.Contracts.Extensions;
using BurgerBackend.Api.Contracts.Handlers.Abstract;
using BurgerBackend.Api.Contracts.Parameters;
using BurgerBackend.Api.Contracts.Results;
using BurgerBackend.Domain.Repositories.Cosmos;

namespace BurgerBackend.Api.Contracts.Handlers.Concrete
{
    public class CreateReviewHandler : ICreateReviewHandler
    {
        private readonly IBurgerPlacesRepository _burgerPlacesRepository;
        private readonly ILogger<CreateReviewHandler> _logger;

        public CreateReviewHandler(IBurgerPlacesRepository burgerPlacesRepository, ILogger<CreateReviewHandler> logger)
        {
            _burgerPlacesRepository = burgerPlacesRepository ?? throw new ArgumentNullException(nameof(burgerPlacesRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CreateReviewResult> ExecuteAsync(CreateReviewParameters parameters, CancellationToken cancellationToken = default)
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
                    return CreateReviewResult.BadRequest(logMessage);
                }

                var place = await _burgerPlacesRepository.GetByIdAsync(parameters.PlaceId.ToString(), cancellationToken);

                if (place is null)
                {
                    return CreateReviewResult.NotFound($"Burger place not found with id: {parameters.PlaceId}");
                }

                var newReview = parameters.Review.ToReview();

                place.Reviews = place.Reviews.Append(newReview);

                await _burgerPlacesRepository.StoreAsync(place, cancellationToken);

                return CreateReviewResult.Ok(newReview.ToReview());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return CreateReviewResult.InternalServerError(e.Message);
            }
        }
    }
}
