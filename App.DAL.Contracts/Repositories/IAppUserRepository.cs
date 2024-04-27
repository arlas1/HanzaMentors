using DALDTO = App.DAL.DTO;
using App.Domain.Identity;
using Base.DAL.Contracts;

namespace App.DAL.Contracts.Repositories;

public interface IAppUserRepository: IBaseEntityRepository<DALDTO.AppUser>
{
    
}