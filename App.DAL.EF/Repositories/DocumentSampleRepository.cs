using App.DAL.Contracts.Repositories;
using App.Domain;
using AutoMapper;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class DocumentSampleRepository : BaseEntityRepository<DomainEntity.DocumentSample, DALDTO.DocumentSample, AppDbContext>, IDocumentSampleRepository
{
    public DocumentSampleRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.DocumentSample, DALDTO.DocumentSample>(mapper))
    {
        
    }
}