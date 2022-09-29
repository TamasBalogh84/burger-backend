using BurgerBackend.Domain.Entities.Cosmos;

namespace BurgerBackend.Domain.Repositories.Cosmos;

public interface IBurgerPlacesRepository : ICosmosRepositoryBase<BurgerPlace>
{
    Task<Review?> GetReviewByIdAsync(Guid reviewId, CancellationToken cancellationToken);

    Task<IEnumerable<BurgerPlace>> GetAllAsync(bool skipReviews, int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    Task<IEnumerable<Review>> GetReviewsByPlaceIdAsync(Guid placeId, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
}