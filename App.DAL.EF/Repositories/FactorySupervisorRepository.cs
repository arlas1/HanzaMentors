using App.DAL.Contracts.Repositories;
using AutoMapper;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class FactorySupervisorRepository :
    BaseEntityRepository<DomainEntity.FactorySupervisor, DALDTO.FactorySupervisor, AppDbContext>, IFactorySupervisorRepository
{
    public FactorySupervisorRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.FactorySupervisor, DALDTO.FactorySupervisor>(mapper))
    {
    }
}