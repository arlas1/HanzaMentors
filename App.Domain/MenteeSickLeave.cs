using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class MenteeSickLeave : BaseEntityId
{
    [Display(ResourceType = typeof(App.Resources.Domain.MenteeSickLeave), Name = nameof(InternMentorshipId))]
    public Guid? InternMentorshipId { get; set; }
    public InternMentorship? InternMentorship { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.MenteeSickLeave), Name = nameof(EmployeeMentorshipId))]
    public Guid? EmployeeMentorshipId { get; set; }
    public EmployeeMentorship? EmployeeMentorship { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.MenteeSickLeave), Name = nameof(FromDate))]
    public DateOnly? FromDate { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.MenteeSickLeave), Name = nameof(UntilDate))]
    public DateOnly? UntilDate { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.MenteeSickLeave), Name = nameof(Reason))]
    public string? Reason { get; set; }
}