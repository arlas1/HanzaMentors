using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Employee : BaseEntityId
{
    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public int EmployeeType	{ get; set; }
    
    [MaxLength(50)]
    public string? Profession { get; set; }
}