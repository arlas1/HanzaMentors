using Base.Domain.Contracts;

namespace Base.DAL.Contracts;

public interface IBaseEntityRepository<TEntity> : IBaseEntityRepository<TEntity, Guid>
    where TEntity : class, IBaseEntityId
{
}

public interface IBaseEntityRepository<TEntity, TKey>
    where TEntity : class, IBaseEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    int Remove(TEntity entity, TKey? userId = default);
    int Remove(TKey id, TKey? userId = default);

    TEntity? FirstOrDefault(TKey id, TKey? userId = default, bool noTracking = true);
    IEnumerable<TEntity> GetAll(TKey? userId = default, bool noTracking = true);
    bool Exists(TKey id, TKey? userId = default);

    Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool noTracking = true);
    Task<IEnumerable<TEntity>> GetAllAsync(TKey? userId = default, bool noTracking = true);
    Task<bool> ExistsAsync(TKey id, TKey? userId = default);
    Task<int> RemoveAsync(TEntity entity, TKey? userId = default);
    Task<int> RemoveAsync(TKey id, TKey? userId = default);
}
