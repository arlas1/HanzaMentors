using System.Diagnostics;
using App.BLL.Contracts;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Spire.Pdf;

namespace WebApp.Controllers;

public class HomeController(IAppBLL bll, UserManager<AppUser> userManager) : Controller
{
    // GET: Intern
    public async Task<IActionResult> Index()
    {
        if (User.Identity!.IsAuthenticated)
        {
            var userId = Guid.Parse(userManager.GetUserId(User)!);
            var interns = await bll.Interns.GetAllAsync(userId);
            return View(interns);
        }
        else
        {
            return View();
        }
    }
}