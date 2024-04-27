using App.DAL.Contracts.Repositories;
using App.Domain;
using AutoMapper;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class InternMentorshipRepository :
    BaseEntityRepository<DomainEntity.InternMentorship, DALDTO.InternMentorship, AppDbContext>, IInternMentorshipRepository
{
    public InternMentorshipRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.InternMentorship, DALDTO.InternMentorship>(mapper))
    {
    }
}