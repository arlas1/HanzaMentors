using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class SickLeaveTypeRepository : BaseEntityRepository<SickLeaveType, AppDbContext>, ISickLeaveTypeRepository
{
    public SickLeaveTypeRepository(AppDbContext dataContext) : base(dataContext)
    {
    }
}