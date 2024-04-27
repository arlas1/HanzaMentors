using App.DAL.Contracts.Repositories;
using AutoMapper;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class EmployeeMentorshipUntilDateRepository :
    BaseEntityRepository<DomainEntity.EmployeeMentorshipUntilDate, DALDTO.EmployeeMentorshipUntilDate, AppDbContext>, IEmployeeMentorshipUntilDateRepository
{
    public EmployeeMentorshipUntilDateRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.EmployeeMentorshipUntilDate, DALDTO.EmployeeMentorshipUntilDate>(mapper))
    {
    }

}