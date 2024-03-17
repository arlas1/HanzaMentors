using Base.DAL.Contracts;
using Base.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseEntityRepository<TEntity, TDbContext> : BaseEntityRepository<TEntity, Guid, TDbContext>,
    IBaseEntityRepository<TEntity>
    where TEntity : class, IBaseEntityId
    where TDbContext : DbContext
{
    public BaseEntityRepository(TDbContext dataContext) : base(dataContext)
    {
    }

}

public class BaseEntityRepository<TEntity, TKey, TDbContext> : IBaseEntityRepository<TEntity, TKey>
    where TEntity : class, IBaseEntityId<TKey>
    where TKey : struct, IEquatable<TKey>
    where TDbContext : DbContext
{
    protected TDbContext DbContext;
    protected DbSet<TEntity> DbSet;

    public BaseEntityRepository(TDbContext dataContext)
    {
        DbContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        DbSet = DbContext.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> AllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public virtual async Task<TEntity?> FindAsync(TKey id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual TEntity Add(TEntity entity)
    {
        return DbSet.Add(entity).Entity;
    }

    public virtual TEntity Update(TEntity entity)
    {
        return DbSet.Update(entity).Entity;
    }

    public virtual TEntity Remove(TEntity entity)
    {
        return DbSet.Remove(entity).Entity;
    }

    public virtual async Task<TEntity?> RemoveAsync(TKey id)
    {
        var entity = await FindAsync(id);
        return entity != null ? Remove(entity) : null;
    }

}