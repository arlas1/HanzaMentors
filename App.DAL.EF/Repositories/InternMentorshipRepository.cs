using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class InternMentorshipRepository : BaseEntityRepository<InternMentorship, AppDbContext>, IInternMentorshipRepository
{
    public InternMentorshipRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
}