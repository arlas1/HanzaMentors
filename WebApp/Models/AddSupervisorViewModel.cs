using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class AddSupervisorViewModel
{
    [Required(ErrorMessage = "Full name is required")]
    [MinLength(1, ErrorMessage = "Full name must be between 1 and 60 characters.")]
    [MaxLength(50, ErrorMessage = "Full name must be between 1 and 60 characters.")]
    public string? FullName { get; set; }
    
    [Required(ErrorMessage = "Supervisor type is required")]
    public string? SupervisorType { get; set; }
}