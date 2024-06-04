using App.BLL.DTO;

namespace WebAppApi.Models;

public class MenteeDTO
{
    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public long PersonalCode { get; set; }
    public string? Profession { get; set; }
    
    public string? MenteeType { get; set; }
    public string? EmployeeType { get; set; }
    public string? InternType { get; set; }
    
    public DateOnly MenteeFromDate { get; set; }
    public DateOnly MenteeUntilDate { get; set; }
    public int MenteeTotalHours { get; set; }
    
    public DateOnly MentorFromDate { get; set; }
    public DateOnly MentorUntilDate { get; set; }
    public int MentorTotalHours { get; set; }

    public string ChosenMentorId { get; set; }
    public string InternFactorySupervisorId { get; set; }
    public string EmployeeFactorySupervisorId { get; set; }
    public string InternSupervisorId { get; set; }
    
    public string IsTest { get; set; }
}