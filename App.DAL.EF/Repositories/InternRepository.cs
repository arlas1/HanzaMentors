using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class InternRepository : BaseEntityRepository<Intern, AppDbContext>, IInternRepository
{
    public InternRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
}