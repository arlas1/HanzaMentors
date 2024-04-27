using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.BLL;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Services;

public class InternMentorshipUntilDateService : 
    BaseEntityService<DALDTO.InternMentorshipUntilDate, BLLDTO.InternMentorshipUntilDate, IInternMentorshipUntilDateRepository>,
    IInternMentorshipUntilDateService
{
    public InternMentorshipUntilDateService(IUnitOfWork uoW, IInternMentorshipUntilDateRepository repository, IMapper mapper) : base(uoW,
        repository, new BLLDALMapper<DALDTO.InternMentorshipUntilDate, BLLDTO.InternMentorshipUntilDate>(mapper))
    {
    }
}