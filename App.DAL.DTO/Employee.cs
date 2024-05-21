using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;
using Base.Domain.Contracts;

namespace App.DAL.DTO;

public class Employee : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public Guid? AppUserId { get; set; }
    
    [Column(TypeName = "jsonb")]
    public LangStr? FirstName { get; set; }
    
    [Column(TypeName = "jsonb")]
    public LangStr? LastName { get; set; }
    public string? EmployeeType { get; set; }
    
    [MaxLength(50)]
    [Column(TypeName = "jsonb")]
    public LangStr? Profession { get; set; }
    public string? Email { get; set; }
}