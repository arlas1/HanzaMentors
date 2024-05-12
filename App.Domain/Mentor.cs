using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Mentor : BaseEntityId
{
    public Guid? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    
    [MaxLength(128)]
    public string? FirstName { get; set; }
    
    [MaxLength(128)]
    public string? LastName { get; set; }
    
    public string? PaymentAmount { get; set; }
    public DateOnly? PaymentOrderDate { get; set; }
    public string? Profession { get; set; }
}