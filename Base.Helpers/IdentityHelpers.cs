using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Base.Helpers;

public static class IdentityHelpers
{
    /// <summary>
    /// asd. qwe Main()
    /// </summary>
    /// <param name="claims"></param>
    /// <param name="key"></param>
    /// <param name="issuer"></param>
    /// <param name="audience"></param>
    /// <param name="expiresInSeconds"></param>
    /// <returns></returns>
    public static string GenerateJwt(IEnumerable<Claim> claims, string key, string issuer, string audience,
        int expiresInSeconds)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddSeconds(expiresInSeconds);
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public static bool ValidateJWT(string jwt, string key, string issuer, string audience)
    {
        var validationParams = new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateIssuerSigningKey = true,

            ValidIssuer = issuer,
            ValidateIssuer = true,

            ValidAudience = audience,
            ValidateAudience = true,

            ValidateLifetime = false
        };

        try
        {
            new JwtSecurityTokenHandler().ValidateToken(jwt, validationParams, out var validatedToken);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

}