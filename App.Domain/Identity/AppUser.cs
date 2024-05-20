using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Base.Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>, IBaseEntityId
{
    [MaxLength(128)]
    [Display(ResourceType = typeof(App.Resources.Domain.AppUser), Name = nameof(FirstName))]
    public string? FirstName { get; set; }
    
    [MaxLength(128)]
    [Display(ResourceType = typeof(App.Resources.Domain.AppUser), Name = nameof(LastName))]
    public string? LastName { get; set; }

    [Display(ResourceType = typeof(App.Resources.Domain.AppUser), Name = nameof(PersonalCode))]
    public long? PersonalCode { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.AppUser), Name = nameof(RefreshTokens))]
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}