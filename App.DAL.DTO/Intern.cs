using Base.Domain.Contracts;

namespace App.DAL.DTO;

public class Intern : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public Guid? AppUserId { get; set; }
    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? InternType { get; set; }
    public string? StudyProgram { get; set; }
    public string? Email { get; set; }
}