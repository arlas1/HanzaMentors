using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class FactorySupervisorRepository : BaseEntityRepository<FactorySupervisor, AppDbContext>, IFactorySupervisorRepository
{
    public FactorySupervisorRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
}