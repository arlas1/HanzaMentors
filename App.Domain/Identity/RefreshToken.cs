using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain.Identity;

public class RefreshToken : BaseRefreshToken
{
    [Display(ResourceType = typeof(App.Resources.Domain.RefreshToken), Name = nameof(AppUserId))]
    public Guid AppUserId { get; set; }
    
    [Display(ResourceType = typeof(App.Resources.Domain.RefreshToken), Name = nameof(AppUser))]
    public AppUser? AppUser { get; set; }
}
