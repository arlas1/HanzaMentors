using Base.Domain.Contracts;

namespace App.BLL.DTO;

public class Intern : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public Guid? AppUserId { get; set; }
    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? InternType { get; set; }
    public string? StudyProgram { get; set; }
}