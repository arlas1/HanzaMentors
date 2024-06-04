using System.ComponentModel.DataAnnotations;
using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public class AddMenteeViewModel
{
    [Required(ErrorMessage = "First name is required")]
    [MinLength(1, ErrorMessage = "First name must be between 1 and 50 characters.")]
    [MaxLength(50, ErrorMessage = "First name must be between 1 and 50 characters.")]
    public string? FirstName { get; set; }
    
    [Required(ErrorMessage = "Last name is required")]
    [MinLength(1, ErrorMessage = "Last name must be between 1 and 50 characters.")]
    [MaxLength(50, ErrorMessage = "Last name must be between 1 and 50 characters.")]
    public string? LastName { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Personal code is required")]
    public long PersonalCode { get; set; }
    
    [Required(ErrorMessage = "Profession is required")]
    [MinLength(1, ErrorMessage = "Profession must be between 1 and 50 characters.")]
    [MaxLength(50, ErrorMessage = "Profession must be between 1 and 50 characters.")]
    public string? Profession { get; set; }
    
    [Required]
    public string? MenteeType { get; set; }
    
    [Required]
    public string? EmployeeType { get; set; }
    
    [Required]
    public string? InternType { get; set; }

    [Required]
    public DateOnly MenteeFromDate { get; set; }
    
    [Required]
    public DateOnly MenteeUntilDate { get; set; }
    
    [Required]
    public DateOnly MentorFromDate { get; set; }
    
    [Required]
    public DateOnly MentorUntilDate { get; set; }
    
    public IEnumerable<FactorySupervisor> FactorySupervisors { get; set; }
    public IEnumerable<InternSupervisor> InternSupervisors { get; set; }
    public IEnumerable<Mentor> Mentors { get; set; }
    public Guid ChosenMentorId { get; set; }
    public Guid InternFactorySupervisorId { get; set; }
    public Guid EmployeeFactorySupervisorId { get; set; }
    public Guid InternSupervisorId { get; set; }
    public int MentorTotalHours { get; set; }
    public int MenteeTotalHours { get; set; }
    public string IsTest { get; set; }
}