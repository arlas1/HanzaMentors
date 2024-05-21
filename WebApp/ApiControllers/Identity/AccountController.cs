using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using App.BLL.Contracts;
using App.DAL.EF;
using App.Domain.Identity;
using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using Base.Helpers;

namespace WebApp.ApiControllers.Identity;

[ApiVersion( "1.0" )]
[ApiController]
[Route("/api/v{version:apiVersion}/identity/[controller]/[action]")]
public class AccountController(UserManager<AppUser> userManager, ILogger<AccountController> logger, 
            SignInManager<AppUser> signInManager, IConfiguration configuration) : ControllerBase
{
    
    /// <summary>
    /// Generates initial jwt token.
    /// </summary>
    /// <param name="expiresInSeconds"></param>
    /// <returns></returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType<JWTResponse>((int) HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
    public async Task<ActionResult<JWTResponse>> GenerateJwt([FromQuery] int expiresInSeconds)
    {
        if (expiresInSeconds <= 0) expiresInSeconds = int.MaxValue;
        expiresInSeconds = expiresInSeconds < configuration.GetValue<int>("JWT:expiresInSeconds")
            ? expiresInSeconds
            : configuration.GetValue<int>("JWT:expiresInSeconds");
        
        var appUser = await userManager.FindByEmailAsync("lasimer0406@gmail.com");
        
        var claimsPrincipal = await signInManager.CreateUserPrincipalAsync(appUser!);
        if (claimsPrincipal == null)
        {
            logger.LogWarning("WebApi login failed, claimsPrincipal null");
            return NotFound("User/Password problem");
        }
        
        var existingClaims = await userManager.GetClaimsAsync(appUser!);
        var existingJwtClaim = existingClaims.FirstOrDefault(c => c.Type == "jwt");
        if (existingJwtClaim != null)
        {
            await userManager.RemoveClaimAsync(appUser!, existingJwtClaim);
        }
        
        var jwt = IdentityHelpers.GenerateJwt(
            claimsPrincipal.Claims,
            configuration.GetValue<string>("JWT:key")!,
            configuration.GetValue<string>("JWT:issuer")!,
            configuration.GetValue<string>("JWT:audience")!,
            expiresInSeconds
        );

        await userManager.AddClaimAsync(appUser!, new Claim("jwt", jwt));
        await signInManager.SignInAsync(appUser!, isPersistent: false);
        
        return Ok();
    }
    
    /// <summary>
    /// Refreshes jwt token when some action on webapp is triggered.
    /// </summary>
    /// <param name="expiresInSeconds"></param>
    /// <returns></returns>
    [HttpPost]
    [Produces("application/json")]
    public async Task<ActionResult<JWTResponse>> RefreshTokenData([FromQuery] int expiresInSeconds)
    {
        if (expiresInSeconds <= 0) expiresInSeconds = int.MaxValue;
        expiresInSeconds = expiresInSeconds < configuration.GetValue<int>("JWT:expiresInSeconds")
            ? expiresInSeconds
            : configuration.GetValue<int>("JWT:expiresInSeconds");

        var user = await userManager.FindByEmailAsync("lasimer0406@gmail.com");
        var existingClaims = await userManager.GetClaimsAsync(user!);
        var existingJwtClaim = existingClaims.FirstOrDefault(c => c.Type == "jwt");
        
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(existingJwtClaim!.Value);
        try
        {
            if (jwt == null)
            {
                return BadRequest(
                    new RestApiErrorResponse()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Error = "No token"
                    }
                );
            }
        }
        catch (Exception e)
        {
            return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "No token"
                }
            );
        }

        if (!IdentityHelpers.ValidateJWT(
                existingJwtClaim!.Value,
                configuration.GetValue<string>("JWT:key")!,
                configuration.GetValue<string>("JWT:issuer")!,
                configuration.GetValue<string>("JWT:audience")!
            ))
        {
            return BadRequest("JWT validation fail");
        }
        
        await userManager.RemoveClaimAsync(user!, existingJwtClaim!);
        var claimsPrincipal = await signInManager.CreateUserPrincipalAsync(user!);

        
        var jwtResponseStr = IdentityHelpers.GenerateJwt(
            claimsPrincipal.Claims,
            configuration.GetValue<string>("JWT:key")!,
            configuration.GetValue<string>("JWT:issuer")!,
            configuration.GetValue<string>("JWT:audience")!,
            expiresInSeconds
        );
        await userManager.AddClaimAsync(user!, new Claim("jwt", jwtResponseStr));
        
        return Ok();
    }
    
    /// <summary>
    /// Deletes the jwt token from claims.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Produces("application/json")]
    public async Task<ActionResult> Logout()
    {
        var appUser = await userManager.FindByEmailAsync("lasimer0406@gmail.com");
        var existingClaims = await userManager.GetClaimsAsync(appUser!);
        var existingJwtClaim = existingClaims.FirstOrDefault(c => c.Type == "jwt");
        
        await userManager.RemoveClaimAsync(appUser!, existingJwtClaim!);
        await signInManager.SignOutAsync();
        
        return Ok();
    }
    
}