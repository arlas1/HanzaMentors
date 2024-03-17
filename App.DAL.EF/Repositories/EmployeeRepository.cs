using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class EmployeeRepository : BaseEntityRepository<Employee, AppDbContext>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
}