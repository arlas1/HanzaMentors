using Base.Domain;

namespace App.Domain;

public class EmployeesMentor : BaseEntityId
{
    public Guid? MentorId { get; set; }
    public Mentor? Mentor { get; set; }
    
    public Guid? EmployeeMentorshipId { get; set; }
    public EmployeeMentorship? EmployeeMentorship { get; set; }
    
    public DateOnly? FromDate { get; set; }
    public DateOnly? UntilDate { get; set; }
    public int? TotalHours { get; set; }
    public bool IsCurrentlyActive { get; set; }
    public string? ChangeReason { get; set; }
}