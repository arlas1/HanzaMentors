using Base.Domain.Contracts;

namespace App.DAL.DTO;

public class EmployeeMentorshipUntilDate : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public Guid EmployeeMentorshipId { get; set; }
    
    public DateOnly UntilDate { get; set; }
}