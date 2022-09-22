using BurgerBackend.Domain.Config;
using BurgerBackend.Domain.Entities.Cosmos;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BurgerBackend.Domain.Repositories.Cosmos;

public class BurgerPlacesRepository : CosmosRepositoryBase<BurgerPlace>, IBurgerPlacesRepository
{
    private readonly ILogger<BurgerPlacesRepository> _logger;
    private readonly Container _burgerPlaceContainer;

    private readonly string _partitionKey;

    public BurgerPlacesRepository(CosmosClient client, IOptions<CosmosConfiguration> cosmosConfiguration,
        ILogger<BurgerPlacesRepository> logger)
        : base(
            client,
            cosmosConfiguration.Value.Database,
            cosmosConfiguration.Value.BurgerPlacesContainer,
            cosmosConfiguration.Value.BurgerPlacesPartitionKey,
            logger)
    {
        _logger = logger;
        _burgerPlaceContainer = client.GetContainer(cosmosConfiguration.Value.Database,
            cosmosConfiguration.Value.BurgerPlacesContainer);
    }

    //public async Task<IEnumerable<BurgerPlace>> GetAllBurgerPlacesAsync()
    //{
    //    var results = new List<BurgerPlace>();
    //    var queryDefinition = new QueryDefinition("SELECT * FROM c");

    //    try
    //    {
    //        var query = this._burgerPlaceContainer.GetItemQueryIterator<BurgerPlace>(queryDefinition);

    //        while (query.HasMoreResults)
    //        {
    //            var response = await query.ReadNextAsync();
    //            results.AddRange(response.ToList());
    //        }
    //    }
    //    catch (CosmosException ex)
    //    {
    //        _logger.LogWarning(ex, $"Couldn't query Burger places in.");
    //        return results;
    //    }

    //    return results;
    //}

    //public async Task<BurgerPlace> GetBurgerPlaceById(Guid id)
    //{
    //    List<BurgerPlace> results = new List<BurgerPlace>();
    //    string query = "SELECT * FROM c WHERE c.id = @id";

    //    QueryDefinition queryDefinition = new QueryDefinition(query)
    //        .WithParameter("@id", id);

    //    // Item stream operations do not throw exceptions for better performance.
    //    // Use GetItemQueryStreamIterator instead of GetItemQueryIterator
    //    //As an exercise change the Get method to use GetItemQueryStreamIterator instead of GetItemQueryIterator

    //    FeedIterator streamResultSet = _burgerPlaceContainer.GetItemQueryStreamIterator(
    //     queryDefinition,
    //     requestOptions: new QueryRequestOptions
    //     {
    //         PartitionKey = new PartitionKey(id.ToString()),
    //         MaxItemCount = 10
    //     });

    //    while (streamResultSet.HasMoreResults)
    //    {
    //        using ResponseMessage responseMessage = await streamResultSet.ReadNextAsync();

    //        if (responseMessage.IsSuccessStatusCode)
    //        {
    //            var streamResponse = FromStream<dynamic>(responseMessage.Content);
    //            var result = streamResponse.Documents.ToObject<List<BurgerPlace>>();
    //            results.AddRange(result);

    //            return results.FirstOrDefault();
    //        }
    //    }

    //    return results.FirstOrDefault();
    //}

    //public async Task AddBurgerPlaceAsync(BurgerPlace place)
    //{
    //    await _burgerPlaceContainer.CreateItemAsync<BurgerPlace>(place, new PartitionKey(place.Id.ToString()));
    //}

    //public async Task UpdateBurgerPlaceAsync(BurgerPlace place)
    //{
    //    if (await GetBurgerPlaceById(place.Id))
    //    {
    //        await _burgerPlaceContainer.ReplaceItemAsync<BurgerPlace>(place, place.Id.ToString(), new PartitionKey(place.Id.ToString()));
    //    }
    //}
    //public async Task DeleteReviewAsync(string id)
    //{
    //    if (await GetFamilyDataFromId(id))
    //    {
    //        await this._container.DeleteItemAsync<Family>(id, new PartitionKey($"{id}"));
    //    }
    //}

    //private static T FromStream<T>(Stream stream)
    //{
    //    using (stream)
    //    {
    //        if (typeof(Stream).IsAssignableFrom(typeof(T)))
    //        {
    //            return (T)(object)stream;
    //        }

    //        using (StreamReader sr = new StreamReader(stream))
    //        {
    //            using (JsonTextReader jsonTextReader = new JsonTextReader(sr))
    //            {
    //                return JsonSerializer.Deserialize<T>(jsonTextReader);
    //            }
    //        }
    //    }
    //}

    public async Task<IEnumerable<Review>> GetReviewsByPlaceIdAsync(Guid placeId, CancellationToken cancellationToken)
    {
        var result = await GetByIdAsync(placeId.ToString(), cancellationToken);

        return result?.Reviews ?? Enumerable.Empty<Review>();
    }

    public async Task<Review?> GetReviewByIdAsync(Guid reviewId, CancellationToken cancellationToken)
    {
        var places = await GetAllAsync(cancellationToken);

        return places.Select(p => p.Reviews.FirstOrDefault(r => r.Id == reviewId)).FirstOrDefault();
    }

    public async Task<IEnumerable<BurgerPlace>> GetAllPlacesWithoutReviews(CancellationToken cancellationToken)
    {
        var result = await GetAllAsync(cancellationToken);

        return result.Select(p => new BurgerPlace()
        {
            Id = p.Id,
            AvailableBurgers = p.AvailableBurgers,
            Information = p.Information,
            Location = p.Location,
            OpeningTime = p.OpeningTime,
            Reviews = Enumerable.Empty<Review>()
        });
    }
}