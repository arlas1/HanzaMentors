using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.BLL;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Services;

public class InternsMentorService : 
    BaseEntityService<DALDTO.InternsMentor, BLLDTO.InternsMentor, IInternsMentorRepository>,
    IInternsMentorService
{
    public InternsMentorService(IUnitOfWork uoW, IInternsMentorRepository repository, IMapper mapper) : base(uoW,
        repository, new BLLDALMapper<DALDTO.InternsMentor, BLLDTO.InternsMentor>(mapper))
    {
    }
}
