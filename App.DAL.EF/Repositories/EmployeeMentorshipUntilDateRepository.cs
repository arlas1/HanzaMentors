using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class EmployeeMentorshipUntilDateRepository : BaseEntityRepository<EmployeeMentorshipUntilDate, AppDbContext>, IEmployeeMentorshipUntilDateRepository
{
    public EmployeeMentorshipUntilDateRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
}