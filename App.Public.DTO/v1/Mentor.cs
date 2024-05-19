﻿using Base.Domain.Contracts;

namespace App.Public.DTO.v1;

public class Mentor : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public Guid? EmployeeId { get; set; }
    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PaymentAmount { get; set; }
    public DateOnly? PaymentOrderDate { get; set; }
    public string? Profession { get; set; }
}