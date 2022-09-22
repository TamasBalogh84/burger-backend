using BurgerBackend.Domain.Entities.Cosmos;

namespace BurgerBackend.Domain.Repositories.Cosmos;

public interface IBurgerPlacesRepository : ICosmosRepositoryBase<BurgerPlace>
{
    Task<IEnumerable<Review>> GetReviewsByPlaceIdAsync(Guid placeId, CancellationToken cancellationToken);

    Task<Review?> GetReviewByIdAsync(Guid reviewId, CancellationToken cancellationToken);

    Task<IEnumerable<BurgerPlace>> GetAllPlacesWithoutReviews(CancellationToken cancellationToken);
}