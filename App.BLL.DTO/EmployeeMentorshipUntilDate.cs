using Base.Domain.Contracts;

namespace App.BLL.DTO;

public class EmployeeMentorshipUntilDate : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public Guid EmployeeMentorshipId { get; set; }
    
    public DateOnly UntilDate { get; set; }
}