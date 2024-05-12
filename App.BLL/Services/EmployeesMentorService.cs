using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.BLL;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Services;


public class EmployeesMentorService : 
    BaseEntityService<DALDTO.EmployeesMentor, BLLDTO.EmployeesMentor, IEmployeesMentorRepository>,
    IEmployeesMentorService
{
    public EmployeesMentorService(IUnitOfWork uoW, IEmployeesMentorRepository repository, IMapper mapper) : base(uoW,
        repository, new BLLDALMapper<DALDTO.EmployeesMentor, BLLDTO.EmployeesMentor>(mapper))
    {
    }
}
