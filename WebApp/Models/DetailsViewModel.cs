using App.DAL.DTO;
using WebApp.DTO;
using Mentor = App.BLL.DTO.Mentor;

namespace WebApp.Models;

public class DetailsViewModel
{
    public Dictionary<Guid, string?> DocumentSamples { get; set; }
    
    public List<Mentor> Mentors { get; set; }
    
    public Guid InitialMentorId { get; set; }
    public string? MentorFirstName { get; set; }
    public string? MentorLastName { get; set; }
    
    public DateOnly? SickLeaveFromDate { get; set; }
    public DateOnly? SickLeaveUntilDate { get; set; }
    public string? SickLeaveReason { get; set; }
    public string? SickLeaveFor { get; set; }
    
    public Guid NewMentorId { get; set; }
    public Guid EmployeeId { get; set; }
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