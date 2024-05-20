using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class FactorySupervisor : BaseEntityId
{
    [MaxLength(50)]
    [Display(ResourceType = typeof(App.Resources.Domain.FactorySupervisor), Name = nameof(FullName))]
    public string? FullName { get; set; }
}