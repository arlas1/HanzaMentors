using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Base.Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>, IBaseEntityId
{
    [MaxLength(128)]
    public string? FirstName { get; set; } = default!;
    
    [MaxLength(128)]
    public string? LastName { get; set; } = default!;
    
    public int PersonalCode { get; set; }
    
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}