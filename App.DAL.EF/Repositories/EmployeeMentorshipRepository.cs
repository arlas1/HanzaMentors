using App.DAL.Contracts.Repositories;
using AutoMapper;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class EmployeeMentorshipRepository : BaseEntityRepository<DomainEntity.EmployeeMentorship, DALDTO.EmployeeMentorship, AppDbContext>, IEmployeeMentorshipRepository
{
    public EmployeeMentorshipRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.EmployeeMentorship, DALDTO.EmployeeMentorship>(mapper))
    {
    }

}