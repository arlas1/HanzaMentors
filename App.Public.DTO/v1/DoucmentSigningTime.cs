using Base.Domain.Contracts;

namespace App.Public.DTO.v1;

public class DoucmentSigningTime : IBaseEntityId
{
    public Guid Id { get; set; }

    public Guid? EmployeeMentorshipDocumentId { get; set; }
    public Guid? InternMentorshipDocumentId { get; set; }

    public string? Time { get; set; }
}