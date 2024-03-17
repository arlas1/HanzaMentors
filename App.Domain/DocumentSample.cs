using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class DocumentSample : BaseEntityId
{
    [MaxLength(50)]
    public string? Title { get; set; }
    
    [Column(TypeName = "bytea")]
    public string? Base64Code { get; set; }
}