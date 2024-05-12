using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Employee : BaseEntityId
{
    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    [MaxLength(128)]
    public string? FirstName { get; set; }
    
    [MaxLength(128)]
    public string? LastName { get; set; }
    
    [MaxLength(50)]
    public string? EmployeeType	{ get; set; }
    
    [MaxLength(50)]
    public string? Profession { get; set; }
}