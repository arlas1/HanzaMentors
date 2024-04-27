using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.DAL.EF;
using DomainEntity = App.Domain.Identity;
using DALDTO = App.DAL.DTO;

namespace App.DAL.EF.Repositories;

public class AppUserRepository : BaseEntityRepository<DomainEntity.AppUser, DALDTO.AppUser, AppDbContext>, IAppUserRepository
{
    public AppUserRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.AppUser, DALDTO.AppUser>(mapper))
    {
    }

}