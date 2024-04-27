using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.BLL;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Services;

public class EmployeeMentorshipUntilDateService : 
    BaseEntityService<DALDTO.EmployeeMentorshipUntilDate, BLLDTO.EmployeeMentorshipUntilDate, IEmployeeMentorshipUntilDateRepository>,
    IEmployeeMentorshipUntilDateService
{
    public EmployeeMentorshipUntilDateService(IUnitOfWork uoW, IEmployeeMentorshipUntilDateRepository repository, IMapper mapper) : base(uoW,
        repository, new BLLDALMapper<DALDTO.EmployeeMentorshipUntilDate, BLLDTO.EmployeeMentorshipUntilDate>(mapper))
    {
    }
}
