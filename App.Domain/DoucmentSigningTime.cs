using Base.Domain;

namespace App.Domain;

public class DoucmentSigningTime : BaseEntityId
{
    public Guid? EmployeeMentorshipDocumentId { get; set; }
    public EmployeeMentorshipDocument? EmployeeMentorshipDocument { get; set; }
    
    public Guid? InternMentorshipDocumentId { get; set; }
    public InternMentorshipDocument? InternMentorshipDocument { get; set; }

    public string? Time { get; set; }
}