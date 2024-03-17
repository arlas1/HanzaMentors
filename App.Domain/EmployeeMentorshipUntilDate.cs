using Base.Domain;

namespace App.Domain;

public class EmployeeMentorshipUntilDate : BaseEntityId
{
    public Guid EmployeeMentorshipId { get; set; }
    public EmployeeMentorship? EmployeeMentorship { get; set; }
    
    public DateOnly UntilDate { get; set; }
}