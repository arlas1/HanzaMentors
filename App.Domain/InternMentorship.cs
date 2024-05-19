using Base.Domain;

namespace App.Domain;

public class InternMentorship : BaseEntityId
{ 
    public Guid? InternId { get; set; }
    public Intern? Intern { get; set; }
    
    public Guid? InternSupervisorId { get; set; }
    public InternSupervisor? InternSupervisor { get; set; }

    public Guid? FactorySupervisorId { get; set; }
    public FactorySupervisor? FactorySupervisor { get; set; }
    
    public DateOnly? FromDate { get; set; }
    public DateOnly? UntilDate { get; set; }
    public int? TotalHours { get; set; }
    public bool IsCurrentlyActive { get; set; }
    public bool CurrentlyOnSickLeave { get; set; }
}