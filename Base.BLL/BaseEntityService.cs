using Base.BLL.Contracts;
using Base.DAL.Contracts;
using Base.Domain.Contracts;

namespace Base.BLL;

public class BaseEntityService<TDalEntity, TBllEntity, TRepository> :
    BaseEntityService<TDalEntity, TBllEntity, TRepository, Guid>,
    IBaseEntityRepository<TBllEntity>
    where TBllEntity : class, IBaseEntityId
    where TRepository : IBaseEntityRepository<TDalEntity, Guid>
    where TDalEntity : class, IBaseEntityId<Guid>
{
    public BaseEntityService(IBaseUnitOfWork uoW, TRepository repository, IBLLMapper<TDalEntity, TBllEntity> Mapper) : base(uoW,
        repository, Mapper)
    {
    }
}

public class BaseEntityService<TDalEntity, TBllEntity, TRepository, TKey> : IEntityService<TBllEntity, TKey>
    where TRepository : IBaseEntityRepository<TDalEntity, TKey>
    where TKey : IEquatable<TKey>
    where TBllEntity : class, IBaseEntityId<TKey>
    where TDalEntity : class, IBaseEntityId<TKey>
{
    private readonly IBaseUnitOfWork UoW;
    protected readonly TRepository Repository;
    protected readonly IBLLMapper<TDalEntity, TBllEntity> Mapper;

    public BaseEntityService(IBaseUnitOfWork uoW, TRepository repository, IBLLMapper<TDalEntity, TBllEntity> mapper)
    {
        UoW = uoW;
        Repository = repository;
        Mapper = mapper;
    }

    public TBllEntity Add(TBllEntity entity)
    {
        return Mapper.Map(Repository.Add(Mapper.Map(entity)!))!;
    }

    public TBllEntity Update(TBllEntity entity)
    {
        return Mapper.Map(Repository.Update(Mapper.Map(entity)!))!;
    }

    public int Remove(TBllEntity entity, TKey? userId = default)
    {
        return Repository.Remove(Mapper.Map(entity)!, userId);
    }

    public int Remove(TKey id, TKey? userId = default)
    {
        return Repository.Remove(id, userId);
    }

    public TBllEntity? FirstOrDefault(TKey id, TKey? userId = default, bool noTracking = true)
    {
        return Mapper.Map(Repository.FirstOrDefault(id, userId, noTracking));
    }

    public IEnumerable<TBllEntity> GetAll(TKey? userId = default, bool noTracking = true)
    {
        return Repository.GetAll(userId, noTracking).Select(e => Mapper.Map(e))!;
    }

    public bool Exists(TKey id, TKey? userId = default)
    {
        return Repository.Exists(id, userId);
    }

    public async Task<bool> ExistsAsync(TKey id, TKey? userId = default)
    {
        return await Repository.ExistsAsync(id, userId);
    }
    
    public async Task<TBllEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool noTracking = true)
    {
        return Mapper.Map(await Repository.FirstOrDefaultAsync(id, userId, noTracking));
    }

    public async Task<IEnumerable<TBllEntity>> GetAllAsync(TKey? userId = default, bool noTracking = true)
    {
        return (await Repository.GetAllAsync(userId, noTracking)).Select(e => Mapper.Map(e))!;
    }

    public async Task<int> RemoveAsync(TBllEntity entity, TKey? userId = default)
    {
        return await Repository.RemoveAsync(Mapper.Map(entity)!, userId);
    }

    public async Task<int> RemoveAsync(TKey id, TKey? userId = default)
    {
        return await Repository.RemoveAsync(id, userId);
    }
}
