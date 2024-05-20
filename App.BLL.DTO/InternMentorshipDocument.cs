using Base.Domain.Contracts;

namespace App.BLL.DTO;

public class InternMentorshipDocument : IBaseEntityId
{
    public Guid Id { get; set; }

    public Guid? InternMentorshipId { get; set; }
    public Guid? DocumentSampleId { get; set; }
    
    public string? Title { get; set; }
    public string? Base64Code { get; set; }
    public string? DocumentStatus { get; set; }
    public string? ChoosenSigningTime { get; set; }
    public string? WayOfSigning { get; set; }
}