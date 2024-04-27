﻿using Base.Domain.Contracts;

namespace App.DAL.DTO;

public class InternMentorshipUntilDate : IBaseEntityId
{
    public Guid Id { get; set; }

    public Guid InternMentorshipId { get; set; }
    
    public DateOnly UntilDate { get; set; }
}