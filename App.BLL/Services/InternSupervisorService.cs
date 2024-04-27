using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.BLL;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Services;

public class InternSupervisorService : 
    BaseEntityService<DALDTO.InternSupervisor, BLLDTO.InternSupervisor, IInternSupervisorRepository>,
    IInternSupervisorService
{
    public InternSupervisorService(IUnitOfWork uoW, IInternSupervisorRepository repository, IMapper mapper) : base(uoW,
        repository, new BLLDALMapper<DALDTO.InternSupervisor, BLLDTO.InternSupervisor>(mapper))
    {
    }
}
