using App.DAL.Contracts.Repositories;
using AutoMapper;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class InternSupervisorRepository :
    BaseEntityRepository<DomainEntity.InternSupervisor, DALDTO.InternSupervisor, AppDbContext>, IInternSupervisorRepository
{
    public InternSupervisorRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.InternSupervisor, DALDTO.InternSupervisor>(mapper))
    {
    }
}