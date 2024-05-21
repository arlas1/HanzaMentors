using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace Base.Tests.Utils;

public class TestEntityService : BaseEntityService<TestDalEntity, TestEntity, IBaseEntityRepository<TestDalEntity, Guid>>
{
    public TestEntityService(IBaseUnitOfWork uow, IBaseEntityRepository<TestDalEntity, Guid> repository, IBLLMapper<TestDalEntity, TestEntity> mapper)
        : base(uow, repository, mapper)
    {
    }
}