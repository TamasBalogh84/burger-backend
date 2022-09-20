using BurgerBackend.Domain.Entities.Cosmos;

namespace BurgerBackend.Domain.Repositories.Cosmos
{
    public interface ICosmosRepositoryBase<TEntity> where TEntity : Entity
    {
        Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);

        Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

        Task<TEntity> StoreAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<bool> ReplaceAsync(TEntity entity, string id, CancellationToken cancellationToken = default);

        Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}