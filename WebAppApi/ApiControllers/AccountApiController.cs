using System.Net;
using App.BLL.Contracts;
using App.DAL.EF;
using App.Domain.Identity;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.DTO;
using WebAppApi.Controllers;
using WebAppApi.Models;

namespace WebAppApi.ApiControllers;


[ApiVersion( "1.0" )]
[ApiController]
[Route("/api/v{version:apiVersion}/[controller]/[action]")]
public class AccountApiController(
    UserManager<AppUser> userManager, ILogger<AccountController> logger, SignInManager<AppUser> signInManager,
    IConfiguration configuration, AppDbContext context, IAppBLL bll) : ControllerBase
{
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> Login([FromBody] LoginDTO loginDto)
    {
        // if (expiresInSeconds <= 0)
        // {
        //     expiresInSeconds = int.MaxValue;
        // }
        //
        // expiresInSeconds = expiresInSeconds < configuration.GetValue<int>("JWT:expiresInSeconds")
        //     ? expiresInSeconds
        //     : configuration.GetValue<int>("JWT:expiresInSeconds");

        var appUser = await userManager.FindByEmailAsync(loginDto.Email);
        if (appUser == null)
        {
            logger.LogWarning("WebApi login failed, email {} not found", loginDto.Email);
            return NotFound("User/Password problem");
        }

        var result = await signInManager.CheckPasswordSignInAsync(appUser, loginDto.Password, false);
        if (!result.Succeeded)
        {
            logger.LogWarning("WebApi login failed, password {} for email {} was wrong", loginDto.Password,
                loginDto.Email);
            return NotFound("User/Password problem");
        }

        var claimsPrincipal = await signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            logger.LogWarning("WebApi login failed, claimsPrincipal null");
            return NotFound("User/Password problem");
        }

        var jwt = IdentityHelpers.GenerateJwt(
            claimsPrincipal.Claims,
            configuration.GetValue<string>("JWT:key"),
            configuration.GetValue<string>("JWT:issuer"),
            configuration.GetValue<string>("JWT:audience"),
            36000
        );

        var refreshToken = new RefreshToken()
        {
            AppUserId = appUser.Id,
            RefreshToken = jwt
        };
        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync();
        
        var responseData = new JWTResponse()
        {
            Jwt = jwt,
            RefreshToken = refreshToken.RefreshToken
        };
        
        var signIn = await signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RememberMe, lockoutOnFailure: false);

        if (signIn.Succeeded)
        {
            return Ok(responseData);
        }
        else
        {
            return BadRequest("Login failed.");
        }
    }
    
    
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType<RefreshToken>((int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public IActionResult GetToken()
    {
        var userIdStr = userManager.GetUserId(User);
        var refreshToken = context.RefreshTokens.FirstOrDefault(token => token.AppUserId.Equals(Guid.Parse(userIdStr)));

        return Ok(refreshToken);
    }
    
    
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> Logout()
    {
        var userId = Guid.Parse(userManager.GetUserId(User));
        var appUser = await bll.Users.FirstOrDefaultAsync(userId);
        if (appUser == null)
        {
            return NotFound(
                new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Error = "User not found"
                }
            );
        }

        var tokensToRemove = context.RefreshTokens.Where(x => x.AppUserId == userId).ToList();
        if (!tokensToRemove.Any())
        {
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = "No matching refresh token found"
            });
        }

        context.RefreshTokens.RemoveRange(tokensToRemove);
        await context.SaveChangesAsync();
        await signInManager.SignOutAsync();
        
        return Ok();
    }

}