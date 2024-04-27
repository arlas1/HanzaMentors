using Base.Domain.Contracts;

namespace App.DAL.DTO;

public class Intern : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public Guid? AppUserId { get; set; }
    
    public int InternType { get; set; }
    public string? StudyProgram { get; set; }
}