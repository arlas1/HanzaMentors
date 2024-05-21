using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;
using Base.Domain.Contracts;

namespace App.BLL.DTO;

public class Intern : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public Guid? AppUserId { get; set; }
    
    [Column(TypeName = "jsonb")]
    public LangStr? FirstName { get; set; }
    
    [Column(TypeName = "jsonb")]
    public LangStr? LastName { get; set; }
    public string? InternType { get; set; }
    
    [Column(TypeName = "jsonb")]
    public LangStr? StudyProgram { get; set; }
    public string? Email { get; set; }
}