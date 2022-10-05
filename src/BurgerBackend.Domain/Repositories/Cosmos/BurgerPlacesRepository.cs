using System.Text;
using BurgerBackend.Domain.Config;
using BurgerBackend.Domain.Entities.Cosmos;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BurgerBackend.Domain.Repositories.Cosmos;

public class BurgerPlacesRepository : CosmosRepositoryBase<BurgerPlace>, IBurgerPlacesRepository
{
    private const string QueryToGetAll = "SELECT * FROM c";

    private const string QueryToGetAllWithoutReviews =
        "SELECT c.id, c.availableBurgers, c.name, c.information, c.location, c.openingTimes FROM c";

    private const string QueryToGetReviews =
        "SELECT * FROM c WHERE c.id = @placeId";

    public BurgerPlacesRepository(CosmosClient client, IOptions<CosmosConfiguration> cosmosConfiguration,
        ILogger<BurgerPlacesRepository> logger)
        : base(
            client,
            cosmosConfiguration.Value.Database,
            cosmosConfiguration.Value.BurgerPlacesContainerName,
            cosmosConfiguration.Value.BurgerPlacesPartitionKey,
            logger)
    {}

    public async Task<IEnumerable<BurgerPlace>> GetAllAsync(string? city, CancellationToken cancellationToken)
    {
        var result = new List<BurgerPlace>();

        var queryDefinition = CreateQueryDefinitionWithCityFilter(city, QueryToGetAll);

        var feedIterator = Container.GetItemQueryIterator<BurgerPlace>(queryDefinition);

        while (feedIterator.HasMoreResults)
        {
            result.AddRange(await feedIterator.ReadNextAsync(cancellationToken));
        }

        return result;
    }

    public async Task<IEnumerable<BurgerPlace>> GetAllWithoutReviewsAsync(string? city, CancellationToken cancellationToken)
    {
        var result = new List<BurgerPlace>();

        var queryDefinition = CreateQueryDefinitionWithCityFilter(city, QueryToGetAllWithoutReviews);

        var feedIterator = Container.GetItemQueryIterator<BurgerPlace>(queryDefinition);

        while (feedIterator.HasMoreResults)
        {
            result.AddRange(await feedIterator.ReadNextAsync(cancellationToken));
        }

        return result;
    }

    public async Task<IEnumerable<Review>> GetReviewsByPlaceIdAsync(Guid placeId, CancellationToken cancellationToken)
    {
        var result = new List<BurgerPlace>();

        var queryDefinition = new QueryDefinition(QueryToGetReviews)
            .WithParameter("@placeId", placeId);

        var feedIterator = Container.GetItemQueryIterator<BurgerPlace>(queryDefinition);

        while (feedIterator.HasMoreResults)
        {
            result.AddRange(await feedIterator.ReadNextAsync(cancellationToken));
        }

        return result.SelectMany(p => p.Reviews ?? Enumerable.Empty<Review>());
    }

    public async Task<Review?> GetReviewByIdAsync(Guid placeId, Guid reviewId, CancellationToken cancellationToken)
    {
        var place = await GetByIdAsync(placeId.ToString(), cancellationToken);

        return place?.Reviews?.FirstOrDefault(r => r.Id == reviewId.ToString());
    }

    private static QueryDefinition CreateQueryDefinitionWithCityFilter(string? city, string query)
    {
        var builder = new StringBuilder(query);

        if (!string.IsNullOrWhiteSpace(city))
        {
            builder.Append(" WHERE c.location.City = @city");
        }

        var queryDefinition = new QueryDefinition(builder.ToString());

        if (!string.IsNullOrWhiteSpace(city))
        {
            queryDefinition.WithParameter("@city", city);
        }

        return queryDefinition;
    }
}