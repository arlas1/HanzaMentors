﻿using Base.Domain.Contracts;

namespace App.BLL.DTO;

public class DoucmentSigningTime : IBaseEntityId
{
    public Guid Id { get; set; }

    public Guid? EmployeeMentorshipDocumentId { get; set; }
    public Guid? InternMentorshipDocumentId { get; set; }

    public string? Time { get; set; }
}