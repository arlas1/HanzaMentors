using System.ComponentModel.DataAnnotations;
using Base.Domain.Contracts;

namespace App.BLL.DTO;

public class Employee : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public Guid? AppUserId { get; set; }
    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmployeeType	{ get; set; }
    
    [MaxLength(50)]
    public string? Profession { get; set; }
}