using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class InternMentorship : BaseEntityId
{ 
    [Display(ResourceType = typeof(App.Resources.Domain.InternMentorship), Name = nameof(InternId))]
    public Guid? InternId { get; set; }
    public Intern? Intern { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.InternMentorship), Name = nameof(InternSupervisorId))]
    public Guid? InternSupervisorId { get; set; }
    public InternSupervisor? InternSupervisor { get; set; }

    [Display(ResourceType = typeof(App.Resources.Domain.InternMentorship), Name = nameof(FactorySupervisorId))]
    public Guid? FactorySupervisorId { get; set; }
    public FactorySupervisor? FactorySupervisor { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.InternMentorship), Name = nameof(FromDate))]
    public DateOnly? FromDate { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.InternMentorship), Name = nameof(UntilDate))]
    public DateOnly? UntilDate { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.InternMentorship), Name = nameof(TotalHours))]
    public int? TotalHours { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.InternMentorship), Name = nameof(IsCurrentlyActive))]
    public bool IsCurrentlyActive { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.InternMentorship), Name = nameof(CurrentlyOnSickLeave))]
    public bool CurrentlyOnSickLeave { get; set; }
}