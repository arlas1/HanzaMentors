using Base.Domain.Contracts;

namespace App.DAL.DTO;

public class EmployeeMentorship : IBaseEntityId
{
    public Guid Id { get; set; }

    public Guid? EmployeeId { get; set; }
    public Guid? FactorySupervisorId { get; set; }
    
    public DateOnly? FromDate { get; set; }
    public DateOnly? UntilDate { get; set; }
    public int? TotalHours { get; set; }
    public bool IsCurrentlyActive { get; set; }
    public bool CurrentlyOnSickLeave { get; set; }
}