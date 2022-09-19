using BurgerBackend.Domain.Entities.Cosmos;
using Microsoft.Azure.Cosmos;

namespace BurgerBackend.Domain.Repositories.Cosmos
{
    public interface ICosmosRepositoryBase<TEntity> where TEntity : Entity
    {
        Task<bool> DeleteAsync(Guid id, string? partitionKeyArea = null, CancellationToken cancellationToken = default);

        Task<IList<TEntity>> GetAllAsync(string? partitionKeyArea = null, CancellationToken cancellationToken = default);

        Task<TEntity?> GetByIdAsync(Guid id, string? partitionKeyArea = null, CancellationToken cancellationToken = default);

        string GetPartitionKeyFromPartitionKeyStruct(PartitionKey input);

        PartitionKey ResolvePartitionKey(string? partitionKeyArea);

        Task<TEntity> StoreAsync(TEntity entity, string? partitionKeyArea = null, CancellationToken cancellationToken = default);

        Task CreateAsync(TEntity entity, string? partitionKeyArea = null,
            CancellationToken cancellationToken = default);
    }
}