using App.DAL.Contracts.Repositories;
using AutoMapper;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class EmployeeMentorshipDocumentRepository :
    BaseEntityRepository<DomainEntity.EmployeeMentorshipDocument, DALDTO.EmployeeMentorshipDocument, AppDbContext>, IEmployeeMentorshipDocumentRepository
{
    public EmployeeMentorshipDocumentRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.EmployeeMentorshipDocument, DALDTO.EmployeeMentorshipDocument>(mapper))
    {
    }


}