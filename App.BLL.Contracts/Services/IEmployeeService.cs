﻿using Base.DAL.Contracts;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Contracts.Services;

public interface IEmployeeService : IBaseEntityRepository<BLLDTO.Employee>
{
    
}