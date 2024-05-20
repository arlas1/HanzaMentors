using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class InternSupervisor : BaseEntityId
{
    [MaxLength(50)]
    [Display(ResourceType = typeof(App.Resources.Domain.InternSupervisor), Name = nameof(FullName))]
    public string? FullName { get; set; }
}