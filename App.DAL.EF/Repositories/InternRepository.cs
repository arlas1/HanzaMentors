using App.DAL.Contracts.Repositories;
using AutoMapper;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class InternRepository : BaseEntityRepository<DomainEntity.Intern, DALDTO.Intern, AppDbContext>, IInternRepository
{
    public InternRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.Intern, DALDTO.Intern>(mapper))
    {
    }
}