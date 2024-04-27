using Base.Domain.Contracts;

namespace App.BLL.DTO;

public class MenteeSickLeave : IBaseEntityId
{
    public Guid Id { get; set; }

    public Guid InternMentorshipId { get; set; }
    public Guid EmployeeMentorshipId { get; set; }
    public Guid SickLeaveTypeId { get; set; }
    
    public DateOnly FromDate { get; set; }
    public DateOnly UntilDate { get; set; }
}