using Base.DAL.Contracts;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Contracts.Services;

public interface IMenteeSickLeaveService : IBaseEntityRepository<BLLDTO.MenteeSickLeave>
{
    
}