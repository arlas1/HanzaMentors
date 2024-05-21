using App.DAL.Contracts.Repositories;
using App.DAL.EF;
using AutoMapper;
using Base.DAL.EF;

namespace Base.Tests.Utils;

public class TestEntityRepository :
    BaseEntityRepository<TestEntity, TestDalEntity, TestDbContext>, ITestEntityRepository
{
    public TestEntityRepository(TestDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<TestEntity, TestDalEntity>(mapper))
    {
    }
}