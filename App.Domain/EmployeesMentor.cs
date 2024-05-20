using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class EmployeesMentor : BaseEntityId
{
    [Display(ResourceType = typeof(App.Resources.Domain.EmployeesMentor), Name = nameof(MentorId))]
    public Guid? MentorId { get; set; }
    public Mentor? Mentor { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.EmployeesMentor), Name = nameof(EmployeeMentorshipId))]
    public Guid? EmployeeMentorshipId { get; set; }
    public EmployeeMentorship? EmployeeMentorship { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.EmployeesMentor), Name = nameof(FromDate))]
    public DateOnly? FromDate { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.EmployeesMentor), Name = nameof(UntilDate))]
    public DateOnly? UntilDate { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.EmployeesMentor), Name = nameof(TotalHours))]
    public int? TotalHours { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.EmployeesMentor), Name = nameof(IsCurrentlyActive))]
    public bool IsCurrentlyActive { get; set; }

    [Display(ResourceType = typeof(App.Resources.Domain.EmployeesMentor), Name = nameof(ChangeReason))]
    public string? ChangeReason { get; set; }
}