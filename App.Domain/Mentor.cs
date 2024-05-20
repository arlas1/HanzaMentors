using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Mentor : BaseEntityId
{
    [Display(ResourceType = typeof(App.Resources.Domain.Mentor), Name = nameof(EmployeeId))]
    public Guid? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    
    [MaxLength(128)]
    [Display(ResourceType = typeof(App.Resources.Domain.Mentor), Name = nameof(FirstName))]
    public string? FirstName { get; set; }
    
    [MaxLength(128)]
    [Display(ResourceType = typeof(App.Resources.Domain.Mentor), Name = nameof(LastName))]
    public string? LastName { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.Mentor), Name = nameof(PaymentAmount))]
    public string? PaymentAmount { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.Mentor), Name = nameof(PaymentOrderDate))]
    public DateOnly? PaymentOrderDate { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.Mentor), Name = nameof(Profession))]
    public string? Profession { get; set; }
}