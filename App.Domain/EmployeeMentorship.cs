using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class EmployeeMentorship : BaseEntityId
{
    [Display(ResourceType = typeof(App.Resources.Domain.EmployeeMentorship), Name = nameof(EmployeeId))]
    public Guid? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.EmployeeMentorship), Name = nameof(FactorySupervisorId))]
    public Guid? FactorySupervisorId { get; set; }
    public FactorySupervisor? FactorySupervisor { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.EmployeeMentorship), Name = nameof(FromDate))]
    public DateOnly? FromDate { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.EmployeeMentorship), Name = nameof(UntilDate))]
    public DateOnly? UntilDate { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.EmployeeMentorship), Name = nameof(TotalHours))]
    public int? TotalHours { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.EmployeeMentorship), Name = nameof(IsCurrentlyActive))]
    public bool IsCurrentlyActive { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.EmployeeMentorship), Name = nameof(CurrentlyOnSickLeave))]
    public bool CurrentlyOnSickLeave { get; set; }
}