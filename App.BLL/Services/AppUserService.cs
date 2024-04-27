using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.BLL;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Services;

public class AppUserService : 
    BaseEntityService<DALDTO.AppUser, BLLDTO.AppUser, IAppUserRepository>,
    IAppUserService
{
    public AppUserService(IUnitOfWork uoW, IAppUserRepository repository, IMapper mapper) : base(uoW,
        repository, new BLLDALMapper<DALDTO.AppUser, BLLDTO.AppUser>(mapper))
    {
    }
}