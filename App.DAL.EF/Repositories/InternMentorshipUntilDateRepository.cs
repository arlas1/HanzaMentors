using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class InternMentorshipUntilDateRepository : BaseEntityRepository<InternMentorshipUntilDate, AppDbContext>, IInternMentorshipUntilDateRepository
{
    public InternMentorshipUntilDateRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
}