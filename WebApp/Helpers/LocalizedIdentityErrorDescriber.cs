using Microsoft.AspNetCore.Identity;

namespace WebApp.Helpers;

public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() => new IdentityError()
    {
        Code = nameof(DefaultError),
        Description = App.Resources.View.Shared._Layout.IdentityError
    };
}
