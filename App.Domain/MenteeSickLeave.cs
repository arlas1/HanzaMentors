using Base.Domain;

namespace App.Domain;

public class MenteeSickLeave : BaseEntityId
{
    public Guid InternMentorshipId { get; set; }
    public InternMentorship? InternMentorship { get; set; }
    
    public Guid EmployeeMentorshipId { get; set; }
    public EmployeeMentorship? EmployeeMentorship { get; set; }

    public Guid SickLeaveTypeId { get; set; }
    public SickLeaveType? SickLeaveType { get; set; }
    
    public DateOnly FromDate { get; set; }
    public DateOnly UntilDate { get; set; }
}