using Base.Domain.Contracts;

namespace Base.Tests.Utils;

public class TestDalEntity : IBaseEntityId
{
    public Guid Id { get; set; }
}
