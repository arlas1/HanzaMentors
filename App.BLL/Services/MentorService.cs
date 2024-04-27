﻿using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.BLL;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

namespace App.BLL.Services;

public class MentorService : 
    BaseEntityService<DALDTO.Mentor, BLLDTO.Mentor, IMentorRepository>,
    IMentorService
{
    public MentorService(IUnitOfWork uoW, IMentorRepository repository, IMapper mapper) : base(uoW,
        repository, new BLLDALMapper<DALDTO.Mentor, BLLDTO.Mentor>(mapper))
    {
    }
}