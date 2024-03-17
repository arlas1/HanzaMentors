using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class EmployeeMentorshipRepository : BaseEntityRepository<EmployeeMentorship, AppDbContext>, IEmployeeMentorshipRepository
{
    public EmployeeMentorshipRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
}