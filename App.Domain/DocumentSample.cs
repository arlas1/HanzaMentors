using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class DocumentSample : BaseEntityId
{
    [MaxLength(50)]
    [Display(ResourceType = typeof(App.Resources.Domain.DocumentSample), Name = nameof(Title))]
    public string? Title { get; set; }
    
    [Column(TypeName = "bytea")]
    [Display(ResourceType = typeof(App.Resources.Domain.DocumentSample), Name = nameof(Base64Code))]
    public string? Base64Code { get; set; }
}