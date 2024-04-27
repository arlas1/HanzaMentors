using Base.DAL.Contracts;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Contracts.Services;

public interface IInternService : IBaseEntityRepository<BLLDTO.Intern>
{
    
}