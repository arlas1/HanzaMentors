using App.DAL.Contracts.Repositories;
using AutoMapper;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class SickLeaveTypeRepository : BaseEntityRepository<DomainEntity.SickLeaveType, DALDTO.SickLeaveType, AppDbContext>, ISickLeaveTypeRepository
{
    public SickLeaveTypeRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.SickLeaveType, DALDTO.SickLeaveType>(mapper))
    {
    }
}