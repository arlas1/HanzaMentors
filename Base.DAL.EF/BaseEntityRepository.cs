using AutoMapper;
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
        // var existingEntity = RepoDbSet.Find(entity.Id); // Retrieve the existing entity from the database
        //
        //
        //     // Map properties from the updated entity to the existing entity
        //     Mapper.Map(entity, existingEntity);
        //
        //     // Update the existing entity in the DbContext
        //     RepoDbSet.Update(existingEntity);
        //
        //     // Save changes to the DbContext
        //     DbContext.SaveChanges();
        //
        //     // Return the existing entity
        //     return existingEntity;       AsNoTracking()
            
        return Mapper.Map(RepoDbSet.Update(Mapper.Map(entity)!).Entity)!;
    }

    public virtual int Remove(TDalEntity entity, TKey? userId = default)
    {
        if (userId == null)
        {
            var entityToRemove = RepoDbSet.AsNoTracking().FirstOrDefault(e => e.Id.Equals(entity.Id));
            RepoDbSet.Remove(entityToRemove!);
        }
        else
        {
            var entityToRemoveWithUser = CreateQuery(userId)
                .FirstOrDefault(e => e.Id.Equals(entity.Id));
            RepoDbSet.Remove(entityToRemoveWithUser!);    
        }
        
        return RepoDbContext.SaveChanges();
    }

    public virtual int Remove(TKey id, TKey? userId = default)
    {
        if (userId == null)
        {
            var entityToRemove = RepoDbSet.FirstOrDefault(e => e.Id.Equals(id));
            RepoDbSet.Remove(entityToRemove!);
        }
        else
        {
            var entityToRemoveWithUser = CreateQuery(userId)
                .FirstOrDefault(e => e.Id.Equals(id));
            RepoDbSet.Remove(entityToRemoveWithUser!);   
        }
        
        return RepoDbContext.SaveChanges();
    }



    public virtual IEnumerable<TDalEntity> GetAll(TKey userId = default, bool noTracking = true)
    {
        return RepoDbSet.AsNoTracking().ToList().Select(de => Mapper.Map(de))!;
        // return CreateQuery(userId, noTracking).ToList().Select(de => Mapper.Map(de))!;
    }

    public virtual bool Exists(TKey id, TKey userId = default)
    {
        // return CreateQuery(userId).Any(e => e.Id.Equals(id));
        return RepoDbSet.Any(e => e.Id.Equals(id));
    }


    public virtual async Task<IEnumerable<TDalEntity?>> GetAllAsync(TKey userId = default, bool noTracking = true)
    {
        return await RepoDbSet.AsNoTracking().Select(de => Mapper.Map(de))!.ToListAsync();
        // return (await CreateQuery(userId, noTracking).ToListAsync())
        //     .Select(de => Mapper.Map(de))!; 
    }

    public virtual async Task<bool> ExistsAsync(TKey id, TKey userId = default)
    {
        // return await CreateQuery(userId).AnyAsync(e => e.Id.Equals(id));
        return await RepoDbSet.AnyAsync(e => e.Id.Equals(id));
    }

    public virtual async Task<int> RemoveAsync(TDalEntity entity, TKey? userId = default)
    {
        if (userId == null)
        {
            var entityToRemove = RepoDbSet.FirstOrDefault(e => e.Id.Equals(entity.Id));
            RepoDbSet.Remove(entityToRemove!);
        }
        else
        {
            var entityToRemove = await CreateQuery(userId)
                .FirstOrDefaultAsync(e => e.Id.Equals(entity.Id));
            RepoDbSet.Remove(entityToRemove!);
        }
    
        return await RepoDbContext.SaveChangesAsync();
    }
    
    public virtual async Task<int> RemoveAsync(TKey id, TKey? userId = default)
    {
        if (userId == null)
        {
            var entityToRemove = await RepoDbSet.FirstOrDefaultAsync(e => e.Id.Equals(id));
            RepoDbSet.Remove(entityToRemove!);
        }
        else
        {
            var entityToRemoveWithUser = await CreateQuery(userId)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            RepoDbSet.Remove(entityToRemoveWithUser!);    
        }
        
        return await RepoDbContext.SaveChangesAsync();
    }
    
    public TDalEntity? FirstOrDefault(TKey id, TKey userId = default, bool noTracking = true)
    {
        return Mapper.Map(RepoDbSet.AsNoTracking().FirstOrDefault(m => m.Id.Equals(id)));
        return Mapper.Map(CreateQuery(userId, noTracking).FirstOrDefault(m => m.Id.Equals(id)));
    }

    public async Task<TDalEntity?> FirstOrDefaultAsync(TKey id, TKey userId = default, bool noTracking = true)
    {
        return Mapper.Map(await RepoDbSet.AsNoTracking().FirstOrDefaultAsync(m => m.Id.Equals(id)));
        return Mapper.Map(await CreateQuery(userId, noTracking).FirstOrDefaultAsync(m => m.Id.Equals(id)));
    }
    
    private IQueryable<TDomainEntity> CreateQuery(TKey? userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (userId != null && !userId.Equals(default) &&
            typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)))
        {
            query = query
                .Include("AppUser")
                .Where(e => ((IDomainAppUserId<TKey>) e).AppUserId.Equals(userId));
        }

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
    
}