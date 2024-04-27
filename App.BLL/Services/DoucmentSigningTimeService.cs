using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.BLL;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Services;

public class DoucmentSigningTimeService : 
    BaseEntityService<DALDTO.DoucmentSigningTime, BLLDTO.DoucmentSigningTime, IDoucmentSigningTimeRepository>,
    IDoucmentSigningTimeService
{
    public DoucmentSigningTimeService(IUnitOfWork uoW, IDoucmentSigningTimeRepository repository, IMapper mapper) : base(uoW,
        repository, new BLLDALMapper<DALDTO.DoucmentSigningTime, BLLDTO.DoucmentSigningTime>(mapper))
    {
    }
}
