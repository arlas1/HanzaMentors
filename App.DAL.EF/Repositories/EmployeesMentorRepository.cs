using App.DAL.Contracts.Repositories;
using AutoMapper;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class EmployeesMentorRepository : BaseEntityRepository<DomainEntity.EmployeesMentor, DALDTO.EmployeesMentor, AppDbContext>, IEmployeesMentorRepository
{
    public EmployeesMentorRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.EmployeesMentor, DALDTO.EmployeesMentor>(mapper))
    {
    }
}