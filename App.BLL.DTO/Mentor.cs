using Base.Domain.Contracts;

namespace App.BLL.DTO;

public class Mentor : IBaseEntityId
{
    public Guid Id { get; set; } // Assuming BaseEntityId includes an Id property
    
    // Foreign keys
    public Guid EmployeeId { get; set; }
    public Guid InternMentorshipId { get; set; }
    public Guid EmployeeMentorshipId { get; set; }
    
    // Navigation properties (if needed)
    // public EmployeeDto Employee { get; set; }
    // public InternMentorshipDto InternMentorship { get; set; }
    // public EmployeeMentorshipDto EmployeeMentorship { get; set; }

    // Other properties
    public DateOnly FromDate { get; set; }
    public DateOnly UntilDate { get; set; }
    public string? PaymentAmount { get; set; }
    public DateOnly PaymentOrderDate { get; set; }
    
    public string? ChangeReason { get; set; }
}