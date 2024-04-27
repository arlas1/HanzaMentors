﻿using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.BLL;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Services;

public class InternService : 
    BaseEntityService<DALDTO.Intern, BLLDTO.Intern, IInternRepository>,
    IInternService
{
    public InternService(IUnitOfWork uoW, IInternRepository repository, IMapper mapper) : base(uoW,
        repository, new BLLDALMapper<DALDTO.Intern, BLLDTO.Intern>(mapper))
    {
    }
}
