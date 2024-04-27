using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.BLL;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Services;

public class EmployeeMentorshipDocumentService : 
    BaseEntityService<DALDTO.EmployeeMentorshipDocument, BLLDTO.EmployeeMentorshipDocument, IEmployeeMentorshipDocumentRepository>,
    IEmployeeMentorshipDocumentService
{
    public EmployeeMentorshipDocumentService(IUnitOfWork uoW, IEmployeeMentorshipDocumentRepository repository, IMapper mapper) : base(uoW,
        repository, new BLLDALMapper<DALDTO.EmployeeMentorshipDocument, BLLDTO.EmployeeMentorshipDocument>(mapper))
    {
    }
}
