using BLLDTO = App.BLL.DTO;
using Base.DAL.Contracts;

namespace App.BLL.Contracts.Services;

public interface IAppUserService : IBaseEntityRepository<BLLDTO.AppUser>
{
    
}