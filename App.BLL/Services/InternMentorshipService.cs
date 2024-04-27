using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.BLL;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Services;

public class InternMentorshipService : 
    BaseEntityService<DALDTO.InternMentorship, BLLDTO.InternMentorship, IInternMentorshipRepository>,
    IInternMentorshipService
{
    public InternMentorshipService(IUnitOfWork uoW, IInternMentorshipRepository repository, IMapper mapper) : base(uoW,
        repository, new BLLDALMapper<DALDTO.InternMentorship, BLLDTO.InternMentorship>(mapper))
    {
    }
}
