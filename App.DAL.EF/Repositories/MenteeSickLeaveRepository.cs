using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class MenteeSickLeaveRepository : BaseEntityRepository<MenteeSickLeave, AppDbContext>, IMenteeSickLeaveRepository
{
    public MenteeSickLeaveRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
}