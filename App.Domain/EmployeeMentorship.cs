using Base.Domain;

namespace App.Domain;

public class EmployeeMentorship : BaseEntityId
{
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    
    public Guid FactorySupervisorId { get; set; }
    public FactorySupervisor? FactorySupervisor { get; set; }
    
    public DateOnly FromDate { get; set; }
    public int TotalHours { get; set; }
}