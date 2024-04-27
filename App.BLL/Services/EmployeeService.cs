using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.BLL;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Services;

public class EmployeeService : 
    BaseEntityService<DALDTO.Employee, BLLDTO.Employee, IEmployeeRepository>,
    IEmployeeService
{
    public EmployeeService(IUnitOfWork uoW, IEmployeeRepository repository, IMapper mapper) : base(uoW,
        repository, new BLLDALMapper<DALDTO.Employee, BLLDTO.Employee>(mapper))
    {
    }
}
