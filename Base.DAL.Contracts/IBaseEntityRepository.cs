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
    Task<IEnumerable<TEntity>> AllAsync();
    Task<TEntity?> FindAsync(TKey id);
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Remove(TEntity entity);
    Task<TEntity?> RemoveAsync(TKey id);

}
