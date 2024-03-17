using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Mentor : BaseEntityId
{
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    
    public Guid InternMentorshipId { get; set; }
    public InternMentorship? InternMentorship { get; set; }

    public Guid EmployeeMentorshipId { get; set; }
    public EmployeeMentorship? EmployeeMentorship { get; set; }

    
    public DateOnly FromDate { get; set; }
    public DateOnly UntilDate { get; set; }
    public string? PaymentAmount { get; set; }
    public DateOnly PaymentOrderDate { get; set; }
    
    [MaxLength(255)]
    public string? ChangeReason { get; set; }
}