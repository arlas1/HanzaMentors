using System.ComponentModel.DataAnnotations;
using Base.Domain.Contracts;

namespace App.BLL.DTO;

public class Employee : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public Guid? AppUserId { get; set; }
    
    public int EmployeeType	{ get; set; }
    
    [MaxLength(50)]
    public string? Profession { get; set; }
}