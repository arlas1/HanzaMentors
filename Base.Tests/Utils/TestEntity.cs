using Base.Domain.Contracts;

namespace Base.Tests.Utils;

public class TestEntity : IBaseEntityId
{
    public Guid Id { get; set; }
}