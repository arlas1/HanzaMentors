using System.Net;
using App.BLL.DTO;
using App.DAL.EF;
using Asp.Versioning;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.ApiControllers.Identity;
using WebApp.DTO;
using WebApp.Models;

namespace WebApp.ApiControllers;

[ApiVersion("1.0")]
[ApiController]
[Route("/api/v{version:apiVersion}/home/[controller]/[action]")]
public class HomeController(UserManager<AppUser> _userManager, ILogger<AccountController> _logger, SignInManager<AppUser> _signInManager,
    IConfiguration _configuration, AppDbContext _context) : ControllerBase
{

}