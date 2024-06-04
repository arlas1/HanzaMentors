using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class AddMentorViewModel
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

    public string IsTest { get; set; }
}