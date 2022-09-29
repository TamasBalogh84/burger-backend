using BurgerBackend.Domain.Config;
using BurgerBackend.Domain.Entities.Cosmos;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BurgerBackend.Domain.Repositories.Cosmos;

public class BurgerPlacesRepository : CosmosRepositoryBase<BurgerPlace>, IBurgerPlacesRepository
{
    public BurgerPlacesRepository(CosmosClient client, IOptions<CosmosConfiguration> cosmosConfiguration,
        ILogger<BurgerPlacesRepository> logger)
        : base(
            client,
            cosmosConfiguration.Value.Database,
            cosmosConfiguration.Value.BurgerPlacesContainerName,
            cosmosConfiguration.Value.BurgerPlacesPartitionKey,
            logger)
    {}

    public async Task<IEnumerable<BurgerPlace>> GetAllAsync(bool skipReviews, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var result = await GetAllAsync(cancellationToken);

        var pagedResult = result
            .OrderBy(bp => bp.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        if (!skipReviews)
        {
            return pagedResult;
        }

        return pagedResult.Select(p => new BurgerPlace
        {
            Id = p.Id,
            AvailableBurgers = p.AvailableBurgers,
            Information = p.Information,
            Location = p.Location,
            OpeningTimes = p.OpeningTimes,
            Reviews = Enumerable.Empty<Review>()
        });
    }

    public async Task<IEnumerable<Review>> GetReviewsByPlaceIdAsync(Guid placeId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var result = await GetByIdAsync(placeId.ToString(), cancellationToken);

        return result?.Reviews
            .OrderBy(r => r.CreatedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize) 
               ?? Enumerable.Empty<Review>();
    }

    public async Task<Review?> GetReviewByIdAsync(Guid reviewId, CancellationToken cancellationToken)
    {
        var places = await GetAllAsync(cancellationToken);

        return places.Select(p => p.Reviews.FirstOrDefault(r => r.Id == reviewId.ToString())).FirstOrDefault();
    }
}