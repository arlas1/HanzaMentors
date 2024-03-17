namespace Base.Domain.Contracts;

public interface IBaseEntityId : IBaseEntityId<Guid>
{
    
}

public interface IBaseEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
}