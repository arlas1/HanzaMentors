using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class EmployeeMentorshipDocumentRepository : BaseEntityRepository<EmployeeMentorshipDocument, AppDbContext>, IEmployeeMentorshipDocumentRepository
{
    public EmployeeMentorshipDocumentRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
}