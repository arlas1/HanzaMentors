using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class InternsMentor : BaseEntityId
{
    [Display(ResourceType = typeof(App.Resources.Domain.InternsMentor), Name = nameof(MentorId))]
    public Guid? MentorId { get; set; }
    public Mentor? Mentor { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.InternsMentor), Name = nameof(InternMentorshipId))]
    public Guid? InternMentorshipId { get; set; }
    public InternMentorship? InternMentorship { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.InternsMentor), Name = nameof(FromDate))]
    public DateOnly? FromDate { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.InternsMentor), Name = nameof(UntilDate))]
    public DateOnly? UntilDate { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.InternsMentor), Name = nameof(TotalHours))]
    public int? TotalHours { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.InternsMentor), Name = nameof(IsCurrentlyActive))]
    public bool IsCurrentlyActive { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.InternsMentor), Name = nameof(ChangeReason))]
    public string? ChangeReason { get; set; }
}