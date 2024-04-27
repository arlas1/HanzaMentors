using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.BLL;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Services;

public class FactorySupervisorService : 
    BaseEntityService<DALDTO.FactorySupervisor, BLLDTO.FactorySupervisor, IFactorySupervisorRepository>,
    IFactorySupervisorService
{
    public FactorySupervisorService(IUnitOfWork uoW, IFactorySupervisorRepository repository, IMapper mapper) : base(uoW,
        repository, new BLLDALMapper<DALDTO.FactorySupervisor, BLLDTO.FactorySupervisor>(mapper))
    {
    }
}
