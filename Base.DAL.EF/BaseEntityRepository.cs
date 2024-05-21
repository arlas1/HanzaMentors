using Base.DAL.Contracts;
using Base.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8601 // Possible null reference assignment.

namespace Base.DAL.EF;

public class BaseEntityRepository<TDomainEntity, TDalEntity, TDbContext> :
    BaseEntityRepository<Guid, TDomainEntity, TDalEntity, TDbContext>, IBaseEntityRepository<TDalEntity>
    where TDomainEntity : class, IBaseEntityId
    where TDalEntity : class, IBaseEntityId
    where TDbContext : DbContext
{
    public BaseEntityRepository(TDbContext dbContext, IDalMapper<TDomainEntity, TDalEntity> mapper) :
        base(dbContext, mapper)
    {
        
    }
}


public class BaseEntityRepository<TKey, TDomainEntity, TDalEntity, TDbContext>
    where TKey : IEquatable<TKey>
    where TDomainEntity : class, IBaseEntityId
    where TDalEntity : class, IBaseEntityId
    where TDbContext : DbContext

{
    protected readonly TDbContext RepoDbContext;
    protected readonly DbSet<TDomainEntity> RepoDbSet;
    protected readonly IDalMapper<TDomainEntity, TDalEntity> Mapper;


    public BaseEntityRepository(TDbContext dbContext, IDalMapper<TDomainEntity,
        TDalEntity> mapper)
    {
        RepoDbContext = dbContext;
        RepoDbSet = RepoDbContext.Set<TDomainEntity>();
        Mapper = mapper;
    }

    public virtual TDalEntity Add(TDalEntity entity)
    {
        return Mapper.Map(RepoDbSet.Add(Mapper.Map(entity)!).Entity)!;
    }

    public virtual TDalEntity Update(TDalEntity entity)
    {
        return Mapper.Map(RepoDbSet.Update(Mapper.Map(entity)!).Entity)!;
    }

    public virtual int Remove(TDalEntity entity, TKey? userId = default)
    {
        var entityToRemove = RepoDbSet.AsNoTracking().FirstOrDefault(e => e.Id.Equals(entity.Id));
        RepoDbSet.Remove(entityToRemove!);
        
        return RepoDbContext.SaveChanges();
    }
    
    public virtual IEnumerable<TDalEntity> GetAll(TKey userId = default, bool noTracking = true)
    {
        return RepoDbSet.AsNoTracking().ToList().Select(de => Mapper.Map(de))!;
    }

    public virtual async Task<IEnumerable<TDalEntity?>> GetAllAsync(TKey userId = default, bool noTracking = true)
    {
        return await RepoDbSet.AsNoTracking().Select(de => Mapper.Map(de))!.ToListAsync();
    }

    public TDalEntity? FirstOrDefault(TKey id, TKey userId = default, bool noTracking = true)
    {
        return Mapper.Map(RepoDbSet.AsNoTracking().FirstOrDefault(m => m.Id.Equals(id)));
    }

    public async Task<TDalEntity?> FirstOrDefaultAsync(TKey id, TKey userId = default, bool noTracking = true)
    {
        return Mapper.Map(await RepoDbSet.AsNoTracking().FirstOrDefaultAsync(m => m.Id.Equals(id)));
    }
}