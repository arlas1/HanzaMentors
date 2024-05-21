using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Intern : BaseEntityId
{
    [Display(ResourceType = typeof(App.Resources.Domain.Intern), Name = nameof(AppUserId))]
    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    [MaxLength(128)]
    [Display(ResourceType = typeof(App.Resources.Domain.Intern), Name = nameof(FirstName))]
    [Column(TypeName = "jsonb")]
    public LangStr? FirstName { get; set; } // 
    
    [MaxLength(128)]
    [Display(ResourceType = typeof(App.Resources.Domain.Intern), Name = nameof(LastName))]
    [Column(TypeName = "jsonb")]
    public LangStr? LastName { get; set; } // 
    
    [MaxLength(128)]
    [Display(ResourceType = typeof(App.Resources.Domain.Intern), Name = nameof(InternType))]
    public string? InternType { get; set; }
    
    [MaxLength(50)]
    [Display(ResourceType = typeof(App.Resources.Domain.Intern), Name = nameof(StudyProgram))]
    [Column(TypeName = "jsonb")]
    public LangStr? StudyProgram { get; set; } // 
    
    [Display(ResourceType = typeof(App.Resources.Domain.Intern), Name = nameof(Email))]
    public string? Email { get; set; }
}