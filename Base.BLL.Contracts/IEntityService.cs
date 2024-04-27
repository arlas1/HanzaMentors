using Base.DAL.Contracts;
using Base.Domain.Contracts;

namespace Base.BLL.Contracts;

public interface IBaseEntityService<TEntity> : IBaseEntityRepository<TEntity>, IEntityService<TEntity, Guid>
    where TEntity : class, IBaseEntityId
{
    
}

public interface IEntityService<TEntity, TKey> : IBaseEntityRepository<TEntity, TKey>
    where TEntity : class, IBaseEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    
}