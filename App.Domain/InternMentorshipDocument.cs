using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class InternMentorshipDocument : BaseEntityId
{
    public Guid? InternMentorshipId { get; set; }
    public InternMentorship? InternMentorship { get; set; }
    
    public Guid? DocumentSampleId { get; set; }
    public DocumentSample? DocumentSample { get; set; }
    
    public Guid? ReceiverId { get; set; }
    
    [Column(TypeName = "bytea")]
    public string? Base64Code { get; set; }
    
    [MaxLength(50)]
    public string? DocumentStatus { get; set; }

    [MaxLength(50)]
    public string? ChoosenSigningTime { get; set; }
    
    public string? WayOfSigning { get; set; }
}