using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class InternSupervisor : BaseEntityId
{
    [MaxLength(50)]
    public string? FullName { get; set; }
}