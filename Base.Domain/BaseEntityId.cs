using Base.Domain.Contracts;

namespace Base.Domain;

public abstract class BaseEntityId : BaseEntityId<Guid>, IBaseEntityId
{
}

public abstract class BaseEntityId<TKey>: IBaseEntityId<TKey> 
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
}