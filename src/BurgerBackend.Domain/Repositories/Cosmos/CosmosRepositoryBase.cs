using BurgerBackend.Domain.Entities.Cosmos;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BurgerBackend.Domain.Repositories.Cosmos
{
    public abstract class CosmosRepositoryBase<TEntity> : ICosmosRepositoryBase<TEntity> where TEntity : Entity
    {
        private readonly ILogger<CosmosRepositoryBase<TEntity>> _logger;

        private readonly string _databaseId;

        private readonly string _containerId;

        private readonly PartitionKey _partitionKey;

        protected readonly CosmosClient CosmosClient;
        
        protected readonly Container Container;

        protected CosmosRepositoryBase(CosmosClient client, string databaseId, string containerId, string partitionKey, ILogger<CosmosRepositoryBase<TEntity>> logger)
        {
            CosmosClient = client ?? throw new ArgumentNullException(nameof(client));

            _partitionKey = string.IsNullOrEmpty(partitionKey)
                ? throw new ArgumentNullException(nameof(partitionKey))
                : new PartitionKey(partitionKey);

            _ = string.IsNullOrEmpty(databaseId)
                ? throw new ArgumentNullException(nameof(databaseId))
                : _databaseId =  databaseId;

            _ = string.IsNullOrEmpty(containerId)
                ? throw new ArgumentNullException(nameof(containerId))
                : _containerId =  containerId;

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            Container = CosmosClient.GetContainer(_databaseId, _containerId);
        }

        public async Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            TEntity? result = default;

            try
            {
                var response = await Container.ReadItemAsync<TEntity>(
                    id,
                    _partitionKey,
                    cancellationToken: cancellationToken);

                return response?.Resource;
            }
            catch (CosmosException ex)
            {
                _logger.LogWarning(ex, $"Entity with id '{id}' not found in `{_databaseId}.{_containerId}`.");
            }

            return result;
        }

        public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = new List<TEntity>();
            var queryDefinition = new QueryDefinition("SELECT * FROM c");
            var queryRequestOptions = new QueryRequestOptions { PartitionKey = _partitionKey };

            try
            {
                var feedIterator = Container.GetItemQueryIterator<TEntity>(queryDefinition, null, queryRequestOptions);
                while (feedIterator.HasMoreResults)
                {
                    result.AddRange(await feedIterator.ReadNextAsync(cancellationToken));
                }
            }
            catch (CosmosException ex)
            {
                _logger.LogWarning(ex, $"Unable to retrieve entities for partitionKey '{_partitionKey}' in `{_databaseId}.{_containerId}`.");
                return result;
            }

            return result;
        }

        public async Task<TEntity> StoreAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                entity.PartitionKey = GetPartitionKeyFromPartitionKeyStruct(_partitionKey);

                var response = await Container.UpsertItemAsync(
                    entity,
                    _partitionKey,
                    cancellationToken: cancellationToken);

                return response.Resource;
            }
            catch (CosmosException ex)
            {
                _logger.LogError(ex, $"Unable to store entity with id `{entity.Id}` in `{_databaseId}.{_containerId}`.");
                throw;
            }
        }

        public async Task<bool> ReplaceAsync(TEntity entity, string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await Container.ReplaceItemAsync<TEntity>(entity, id, _partitionKey, cancellationToken: cancellationToken);

                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (CosmosException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }

                _logger.LogError(ex, $"Unable to delete entity with id `{id}` in `{_databaseId}.{_containerId}`.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await Container.DeleteItemAsync<TEntity>(
                    id,
                    _partitionKey,
                    cancellationToken: cancellationToken);

                return response.StatusCode == HttpStatusCode.NoContent;
            }
            catch (CosmosException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }

                _logger.LogError(ex, $"Unable to delete entity with id `{id}` in `{_databaseId}.{_containerId}`.");
                throw;
            }
        }

        public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                entity.PartitionKey = GetPartitionKeyFromPartitionKeyStruct(_partitionKey);

                await Container.CreateItemAsync(
                    entity,
                    _partitionKey,
                    new ItemRequestOptions { EnableContentResponseOnWrite = false },
                    cancellationToken: cancellationToken);
            }
            catch (CosmosException ex)
            {
                _logger.LogError(ex, $"Unable to create entity with id `{entity.Id}` in `{_databaseId}.{_containerId}`.");
                throw;
            }
        }

        private string GetPartitionKeyFromPartitionKeyStruct(PartitionKey input)
            => input
                .ToString()
                .Replace("[\"", string.Empty, StringComparison.Ordinal)
                .Replace("\"]", string.Empty, StringComparison.Ordinal);
    }
}
