using System.Data.Common;
using App.DAL.Contracts;
using Base.BLL.Contracts;
using Base.DAL.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace Base.BLL;

public abstract class BaseBLL<TAppDbContext> : IBLL
     where TAppDbContext : DbContext
{
    protected readonly IUnitOfWork UoW;

    protected BaseBLL(IUnitOfWork unitOfWork)
    {
        UoW = unitOfWork;
    }
    public async Task<int> SaveChangesAsync()
    {
        return await UoW.SaveChangesAsync();
    }
}