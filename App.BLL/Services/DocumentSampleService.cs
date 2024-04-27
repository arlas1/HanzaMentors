using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.BLL;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Services;

public class DocumentSampleService : 
    BaseEntityService<DALDTO.DocumentSample, BLLDTO.DocumentSample, IDocumentSampleRepository>,
    IDocumentSampleService
{
    public DocumentSampleService(IUnitOfWork uoW, IDocumentSampleRepository repository, IMapper mapper) : base(uoW,
        repository, new BLLDALMapper<DALDTO.DocumentSample, BLLDTO.DocumentSample>(mapper))
    {
    }
}
