using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Employee : BaseEntityId
{
    [Display(ResourceType = typeof(App.Resources.Domain.Employee), Name = nameof(AppUserId))]
    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    [MaxLength(128)]
    [Display(ResourceType = typeof(App.Resources.Domain.AppUser), Name = nameof(FirstName))]
    // [Column(TypeName = "jsonb")]
    public string? FirstName { get; set; }
    
    [MaxLength(128)]
    [Display(ResourceType = typeof(App.Resources.Domain.AppUser), Name = nameof(LastName))]
    public string? LastName { get; set; }
    
    [MaxLength(50)]
    [Display(ResourceType = typeof(App.Resources.Domain.Employee), Name = nameof(EmployeeType))]
    public string? EmployeeType	{ get; set; }
    
    [MaxLength(50)]
    [Display(ResourceType = typeof(App.Resources.Domain.Employee), Name = nameof(Profession))]
    public string? Profession { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.Employee), Name = nameof(Email))]
    public string? Email { get; set; }
}