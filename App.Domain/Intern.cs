using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Intern : BaseEntityId
{
    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public int InternType { get; set; }
    
    [MaxLength(50)]
    public string? StudyProgram { get; set; }
}