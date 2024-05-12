using App.DAL.Contracts.Repositories;
using AutoMapper;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class InternsMentorRepository : BaseEntityRepository<DomainEntity.InternsMentor, DALDTO.InternsMentor, AppDbContext>, IInternsMentorRepository
{
    public InternsMentorRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.InternsMentor, DALDTO.InternsMentor>(mapper))
    {
    }
}