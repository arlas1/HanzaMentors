using Base.DAL.Contracts;

namespace Base.Tests.Utils;

public class TestDalMapper : IDalMapper<TestEntity, TestDalEntity>
{
    public TestEntity Map(TestDalEntity dalEntity)
    {
        return new TestEntity { Id = dalEntity.Id };
    }

    public TestDalEntity Map(TestEntity domainEntity)
    {
        return new TestDalEntity { Id = domainEntity.Id };
    }
}