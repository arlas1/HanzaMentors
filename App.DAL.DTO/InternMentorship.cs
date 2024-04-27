using Base.Domain.Contracts;

namespace App.DAL.DTO;

public class InternMentorship : IBaseEntityId
{ 
    public Guid Id { get; set; }
    
    public Guid InternId { get; set; }
    public Guid InternSupervisorId { get; set; }
    public Guid FactorySupervisorId { get; set; }
    
    public DateOnly FromDate { get; set; }
    public int TotalHours { get; set; }
}