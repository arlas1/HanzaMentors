﻿using Base.Domain.Contracts;

namespace App.DAL.DTO;

public class Mentor : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public Guid EmployeeId { get; set; }
    public Guid InternMentorshipId { get; set; }
    public Guid EmployeeMentorshipId { get; set; }
    
    public DateOnly FromDate { get; set; }
    public DateOnly UntilDate { get; set; }
    public string? PaymentAmount { get; set; }
    public DateOnly PaymentOrderDate { get; set; }
    public string? ChangeReason { get; set; }
}
