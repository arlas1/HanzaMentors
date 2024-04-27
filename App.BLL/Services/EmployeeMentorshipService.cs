using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.BLL;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Services;

public class EmployeeMentorshipService : 
    BaseEntityService<DALDTO.EmployeeMentorship, BLLDTO.EmployeeMentorship, IEmployeeMentorshipRepository>,
    IEmployeeMentorshipService
{
    public EmployeeMentorshipService(IUnitOfWork uoW, IEmployeeMentorshipRepository repository, IMapper mapper) : base(uoW,
        repository, new BLLDALMapper<DALDTO.EmployeeMentorship, BLLDTO.EmployeeMentorship>(mapper))
    {
    }
}
