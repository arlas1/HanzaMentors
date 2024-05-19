﻿using Base.Domain.Contracts;

namespace App.Public.DTO.v1;

public class EmployeesMentor : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public Guid? MentorId { get; set; }
    public Guid? EmployeeMentorshipId { get; set; }
    
    public DateOnly? FromDate { get; set; }
    public DateOnly? UntilDate { get; set; }
    public int? TotalHours { get; set; }
    public bool IsCurrentlyActive { get; set; }
    public string? ChangeReason { get; set; }
}