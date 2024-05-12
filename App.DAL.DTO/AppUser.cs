using App.Domain.Identity;
using Base.Domain.Contracts;

namespace App.DAL.DTO;

public class AppUser : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public string? FirstName { get; set; } = default!;
    public string? LastName { get; set; } = default!;
    public long? PersonalCode { get; set; }
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}