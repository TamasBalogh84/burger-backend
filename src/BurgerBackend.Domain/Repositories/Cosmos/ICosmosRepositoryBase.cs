using BurgerBackend.Domain.Entities.Cosmos;

namespace BurgerBackend.Domain.Repositories.Cosmos
{
    public interface ICosmosRepositoryBase<TEntity> where TEntity : Entity
    {
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<TEntity> StoreAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}