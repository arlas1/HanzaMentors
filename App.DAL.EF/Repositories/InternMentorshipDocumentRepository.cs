using App.DAL.Contracts.Repositories;
using AutoMapper;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class InternMentorshipDocumentRepository :
    BaseEntityRepository<DomainEntity.InternMentorshipDocument, DALDTO.InternMentorshipDocument, AppDbContext>, IInternMentorshipDocumentRepository
{
    public InternMentorshipDocumentRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.InternMentorshipDocument, DALDTO.InternMentorshipDocument>(mapper))
    {
    }
}