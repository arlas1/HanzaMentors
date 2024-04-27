using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.BLL;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Services;

public class InternMentorshipDocumentService : 
    BaseEntityService<DALDTO.InternMentorshipDocument, BLLDTO.InternMentorshipDocument, IInternMentorshipDocumentRepository>,
    IInternMentorshipDocumentService
{
    public InternMentorshipDocumentService(IUnitOfWork uoW, IInternMentorshipDocumentRepository repository, IMapper mapper) : base(uoW,
        repository, new BLLDALMapper<DALDTO.InternMentorshipDocument, BLLDTO.InternMentorshipDocument>(mapper))
    {
    }
}
