using App.Domain;
using Base.Domain.Contracts;

namespace App.DAL.DTO;

public class MenteeSickLeave : IBaseEntityId
{
    public Guid Id { get; set; }

    public Guid? InternMentorshipId { get; set; }
    public Guid? EmployeeMentorshipId { get; set; }
    
    public DateOnly? FromDate { get; set; }
    public DateOnly? UntilDate { get; set; }
    public string? Reason { get; set; }
}