using App.DAL.Contracts.Repositories;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;using AutoMapper;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class MenteeSickLeaveRepository :
    BaseEntityRepository<DomainEntity.MenteeSickLeave, DALDTO.MenteeSickLeave, AppDbContext>, IMenteeSickLeaveRepository
{
    public MenteeSickLeaveRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.MenteeSickLeave, DALDTO.MenteeSickLeave>(mapper))
    {
    }
}