using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class DoucmentSigningTime : BaseEntityId
{
    [Display(ResourceType = typeof(App.Resources.Domain.DoucmentSigningTime), Name = nameof(EmployeeMentorshipDocumentId))]
    public Guid? EmployeeMentorshipDocumentId { get; set; }
    public EmployeeMentorshipDocument? EmployeeMentorshipDocument { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.DoucmentSigningTime), Name = nameof(InternMentorshipDocumentId))]
    public Guid? InternMentorshipDocumentId { get; set; }
    public InternMentorshipDocument? InternMentorshipDocument { get; set; }

    [Display(ResourceType = typeof(App.Resources.Domain.DoucmentSigningTime), Name = nameof(Time))]
    public string? Time { get; set; }
}