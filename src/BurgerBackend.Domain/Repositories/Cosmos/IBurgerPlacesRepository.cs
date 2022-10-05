using BurgerBackend.Domain.Entities.Cosmos;

namespace BurgerBackend.Domain.Repositories.Cosmos;

public interface IBurgerPlacesRepository : ICosmosRepositoryBase<BurgerPlace>
{
    Task<Review?> GetReviewByIdAsync(Guid placeId, Guid reviewId, CancellationToken cancellationToken);

    Task<IEnumerable<BurgerPlace>> GetAllAsync(string? city, CancellationToken cancellationToken);

    Task<IEnumerable<BurgerPlace>> GetAllWithoutReviewsAsync(string? city, CancellationToken cancellationToken);

    Task<IEnumerable<Review>> GetReviewsByPlaceIdAsync(Guid placeId, CancellationToken cancellationToken);
}