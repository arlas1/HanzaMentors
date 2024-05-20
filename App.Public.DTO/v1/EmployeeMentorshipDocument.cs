using Base.Domain.Contracts;

namespace App.Public.DTO.v1;

public class EmployeeMentorshipDocument : IBaseEntityId
{
    public Guid Id { get; set; }

    public Guid? EmployeeMentorshipId { get; set; }
    public Guid? DocumentSampleId { get; set; }
    
    public string? Title { get; set; }
    public string? Base64Code { get; set; }
    public string? DocumentStatus { get; set; }
    public string? ChoosenSigningTime { get; set; }
    public string? WayOfSigning { get; set; } 
}