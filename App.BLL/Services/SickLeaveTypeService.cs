﻿using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.BLL;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Services;

public class SickLeaveTypeService : 
    BaseEntityService<DALDTO.SickLeaveType, BLLDTO.SickLeaveType, ISickLeaveTypeRepository>,
    ISickLeaveTypeService
{
    public SickLeaveTypeService(IUnitOfWork uoW, ISickLeaveTypeRepository repository, IMapper mapper) : base(uoW,
        repository, new BLLDALMapper<DALDTO.SickLeaveType, BLLDTO.SickLeaveType>(mapper))
    {
    }
}