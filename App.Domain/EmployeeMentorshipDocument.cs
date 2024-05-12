using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class EmployeeMentorshipDocument : BaseEntityId
{
    public Guid? EmployeeMentorshipId { get; set; }
    public EmployeeMentorship? EmployeeMentorship { get; set; }

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