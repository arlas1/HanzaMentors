using App.DAL.Contracts.Repositories;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;using AutoMapper;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class InternMentorshipUntilDateRepository :
    BaseEntityRepository<DomainEntity.InternMentorshipUntilDate, DALDTO.InternMentorshipUntilDate, AppDbContext>, IInternMentorshipUntilDateRepository
{
    public InternMentorshipUntilDateRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.InternMentorshipUntilDate, DALDTO.InternMentorshipUntilDate>(mapper))
    {
    }
}