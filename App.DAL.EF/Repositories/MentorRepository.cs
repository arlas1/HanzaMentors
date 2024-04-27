﻿using App.DAL.Contracts.Repositories;
using AutoMapper;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class MentorRepository : BaseEntityRepository<DomainEntity.Mentor, DALDTO.Mentor, AppDbContext>,
    IMentorRepository
{
    public MentorRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDummyMapper<DomainEntity.Mentor, DALDTO.Mentor>(mapper))
    {
    }
}