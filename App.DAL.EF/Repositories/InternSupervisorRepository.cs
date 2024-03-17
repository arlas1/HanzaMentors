using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class InternSupervisorRepository : BaseEntityRepository<InternSupervisor, AppDbContext>, IInternSupervisorRepository
{
    public InternSupervisorRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
}