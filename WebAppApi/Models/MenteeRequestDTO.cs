using App.BLL.DTO;

namespace WebAppApi.Models;

public class MenteeRequestDTO
{
    public string MenteeId { get; set; }
    
    public List<Mentor> Mentors { get; set; }
    public string InitialMentorId { get; set; }
    public string? MentorFirstName { get; set; }
    public string? MentorLastName { get; set; }
    
    public string NewMentorId { get; set; }
    public string? EmployeeFirstName { get; set; }
    public string? EmployeeLastName { get; set; }
    public string? EmployeeProfession { get; set; }
    public DateOnly? EmployeeFromDate { get; set; }
    public DateOnly? EmployeeUntilDate { get; set; }
    public int? EmployeeTotalHours { get; set; }
    public DateOnly? EmployeeMentorFromDate { get; set; }
    public DateOnly? EmployeeMentorUntilDate { get; set; }
    public int? EmployeeMentorTotalHours { get; set; }
    public string? ChangeReason { get; set; }
    
    public DateOnly? NewMentorFromDate { get; set; }
    public DateOnly? NewMentorUntilDate { get; set; }
    public int? NewMentorTotalHours { get; set; }
}