using App.DAL.Contracts.Repositories;
using AutoMapper;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class DoucmentSigningTimeRepository : BaseEntityRepository<DomainEntity.DoucmentSigningTime, DALDTO.DoucmentSigningTime, AppDbContext>, IDoucmentSigningTimeRepository
{
    public DoucmentSigningTimeRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.DoucmentSigningTime, DALDTO.DoucmentSigningTime>(mapper))
    {
        
    }
}