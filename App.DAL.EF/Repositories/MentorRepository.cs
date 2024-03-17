using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class MentorRepository : BaseEntityRepository<Mentor, AppDbContext>, IMentorRepository
{
    public MentorRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
}