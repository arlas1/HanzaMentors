using App.DAL.Contracts.Repositories;
using AutoMapper;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class EmployeeRepository : BaseEntityRepository<DomainEntity.Employee, DALDTO.Employee, AppDbContext>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.Employee, DALDTO.Employee>(mapper))
    {
    }

}